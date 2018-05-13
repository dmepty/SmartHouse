using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Input;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Net;
using PropertyChanged;
using SmartHouse.Commands;
using SmartHouse.Models;
using LiveCharts.Wpf;
using LiveCharts;

namespace SmartHouse.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    class MainViewModel
    {
        #region Переменные
        //Коллекции для графиков
        public SeriesCollection SeriesTemperature { get; set; }
        public SeriesCollection SeriesHumidity { get; set; }
        public SeriesCollection SeriesPressure { get; set; }

        //Дата замера
        public List<string> Labels { get; set; } = new List<string>();

        //Форматы данных
        public Func<double, string> YFormatterTemperature { get; set; }
        public Func<double, string> YFormatterHumidity { get; set; }
        public Func<double, string> YFormatterPressure { get; set; }

        //Старые показатели
        public List<ValueParameters> ValueTemperature { get; set; }
        public List<ValueParameters> ValueHumidity { get; set; }
        public List<ValueParameters> ValuePressure { get; set; }

        //Текущие показатели
        public string ValueCurrentTemperature { get; set; }
        public string ValueCurrentHumidity { get; set; }
        public string ValueCurrentPressure { get; set; }

        List<double> valueTemp = new List<double>();
        List<double> valueHum = new List<double>();
        List<double> valuePres = new List<double>();

        List<DateTime> valueDate = new List<DateTime>();

        private readonly BackgroundWorker _backgroundWorker;

        public bool FirstRelayStatus { get ; set; }
        public bool SecondRelayStatus { get; set; }



        private ICommand _showDeviceWindow;
        private ICommand _showTemplateDeviceWindow;
        private ICommand _showTemplateFirmwareWindow;
        private ICommand _showSensorWindow;
        private ICommand _showParameterWindow;
        private ICommand _setFirstRelayStatusCommand;
        private ICommand _setSecondRelayStatusCommand;
        #endregion

        public MainViewModel()
        {
            //Создание графиков
            SeriesTemperature = new SeriesCollection
            {
                new LineSeries
                {
                    Title="Температура",
                    Values = new ChartValues<double>()
                }
            };
            SeriesHumidity = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Влажность",
                    Values = new ChartValues<double>()
                }
            };
            SeriesPressure = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Давление",
                    Values = new ChartValues<double>()
                }
            };

            try
            {
                //Получение значений показателей
                ValueTemperature = BackClient.GetEntities<List<ValueParameters>>("json_old_parameters_temp.php");
                ValueHumidity = BackClient.GetEntities<List<ValueParameters>>("json_old_parameters_hum.php");
                ValuePressure = BackClient.GetEntities<List<ValueParameters>>("json_old_parameters_pres.php");

                //получение статуса реле
                var RelaysStatus = BackClient.GetEntities<string>("get_rele_status.php");

                FirstRelayStatus = RelaysStatus[0] != '0';
                SecondRelayStatus = RelaysStatus[1] != '0';
            }
            catch (WebException ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (Newtonsoft.Json.JsonException)
            {
                MessageBox.Show("Ошибка получения данных!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            //Запись температуры в массивы
            foreach (var values in ValueTemperature)
            {
                valueTemp.Add(values.Value);
                valueDate.Add(values.Date);
            }

            //Запись влажности в массивы
            foreach (var values in ValueHumidity)
                valueHum.Add(values.Value);

            //Запись давления в массивы
            foreach (var values in ValuePressure)
                valuePres.Add(values.Value);

            //Присваивание значение массивов графику
            for (int i = 0; i < valueTemp.Count; i++)
            {
                SeriesTemperature[0].Values.Add(valueTemp[i]);
                SeriesHumidity[0].Values.Add(valueHum[i]);
                SeriesPressure[0].Values.Add(valuePres[i]);

                Labels.Add(valueDate[i].ToString("dd.MM.yyyy HH:mm (МСК)"));
            }


            YFormatterTemperature = value => value.ToString("0.0°");
            YFormatterHumidity = value => (value / 100).ToString("#%");
            YFormatterPressure = value => value.ToString("0 мм рт столба");

            _backgroundWorker = new BackgroundWorker { WorkerSupportsCancellation = true };
            _backgroundWorker.DoWork += (s, a) =>
            {
                while (true)
                {
                    RefreshTemperature();
                    if (_backgroundWorker.CancellationPending)
                        break;

                    Thread.Sleep(30000);
                }
            };


            _backgroundWorker.RunWorkerAsync();
        }

        public void RefreshTemperature()
        {
            try
            {
                ValueTemperature = BackClient.GetEntities<List<ValueParameters>>("json_old_parameters_temp.php");
                ValueHumidity = BackClient.GetEntities<List<ValueParameters>>("json_old_parameters_hum.php");
                ValuePressure = BackClient.GetEntities<List<ValueParameters>>("json_old_parameters_pres.php");

                //Запись температуры в массивы
                foreach (var values in ValueTemperature)
                {
                    valueTemp.Add(values.Value);
                    valueDate.Add(values.Date);
                }

                //Запись влажности в массивы
                foreach (var values in ValueHumidity)
                    valueHum.Add(values.Value);

                //Запись давления в массивы
                foreach (var values in ValuePressure)
                    valuePres.Add(values.Value);


                //Получение текущих значений
                var valueCurrentTemperature = BackClient.GetEntities<List<ValueParameters>>("json_current_parameters_temp.php");
                var valueCurrentHumidity = BackClient.GetEntities<List<ValueParameters>>("json_current_parameters_hum.php");
                var valueCurrentPressure = BackClient.GetEntities<List<ValueParameters>>("json_current_parameters_pres.php");

                ValueCurrentTemperature = valueCurrentTemperature[0].Value.ToString("0.0°");
                ValueCurrentHumidity = (valueCurrentHumidity[0].Value / 100).ToString("#%");
                ValueCurrentPressure = valueCurrentPressure[0].Value.ToString("0 мм рт столба");


                //Работа в трее
                if (Int32.Parse(valueCurrentTemperature[0].Value.ToString()) > 35)
                {
                    ClosingBehavior.NotifyIcon.BalloonTipText =
                        @"Температура превысила допустимы значения! Температура: " +
                        valueCurrentTemperature[0].Value;
                    ClosingBehavior.NotifyIcon.ShowBalloonTip(1000);
                }


                //Обновление графиков
                if (ValueTemperature.Count > 10)
                {
                    SeriesTemperature[0].Values.Add(valueTemp.Last());
                    SeriesHumidity[0].Values.Add(valueHum.Last());
                    SeriesPressure[0].Values.Add(valuePres.Last());

                    Labels.Add(valueDate.Last().ToString("dd.MM.yyyy HH:mm (МСК)"));

                    Labels.RemoveAt(0);
                    SeriesTemperature[0].Values.RemoveAt(0);
                    SeriesHumidity[0].Values.RemoveAt(0);
                    SeriesPressure[0].Values.RemoveAt(0);


                    BackClient.SendRequest("http://h92761ae.beget.tech/delete_old_parameter.php?");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка! " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

                _backgroundWorker.CancelAsync();
            }

        }

        public ICommand ShowDeviceWindow => _showDeviceWindow ?? 
                                            (_showDeviceWindow = new OpenDeviceWindow());

        public ICommand ShowTemplateDeviceWindow => _showTemplateDeviceWindow ?? 
                                                    (_showTemplateDeviceWindow = new OpenTemplateDeviceWindow());

        public ICommand ShowTemplateFirmwareCommand => _showTemplateFirmwareWindow ?? 
                                                       (_showTemplateFirmwareWindow = new OpenTemplateFirmwareWindow());

        public ICommand ShowSensorWindow => _showSensorWindow ?? 
                                            (_showSensorWindow = new OpenSensorWindow());

        public ICommand ShowParameterWindow => _showParameterWindow ?? 
                                               (_showParameterWindow = new OpenParameterWindow());

        public ICommand SetFirstRelayStatusCommand => _setFirstRelayStatusCommand ??
                                                      (_setFirstRelayStatusCommand = new SetFirstRelayStatusCommand(this));

        public ICommand SetSecondRelayStatusCommand => _setSecondRelayStatusCommand ??
                                                       (_setSecondRelayStatusCommand = new SetSecondRelayStatusCommand(this));
    }
}
