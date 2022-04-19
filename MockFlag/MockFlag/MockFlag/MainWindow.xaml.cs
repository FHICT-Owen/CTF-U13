﻿using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MQTTnet;
using Newtonsoft.Json;

namespace MockFlag;

public partial class MainWindow : Window
{
    private string _currentCode;
    int currentTeam = 0;
    int capturedAmount = 1;
    int capturedTeam = 0;

    MQTTClient client;

    private readonly string CODERED = "1234";
    private readonly string CODEBLUE = "5678";

    public MainWindow()
    {
        _currentCode = "";
        InitializeComponent();
        Label.Content = "Start entering code!";

        client = new MQTTClient("test2", Host: "localhost");
        client.SetSubscribeTopic("#");
        client.ConnectAsync();


    }

    #region KeyPad
    private void _1_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "1";
        HandleCodeEntry(sender, e);
    }

    private void _2_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "2";
        HandleCodeEntry(sender, e);
    }

    private void _3_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "3";
        HandleCodeEntry(sender, e);
    }

    private void _4_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "4";
        HandleCodeEntry(sender, e);
    }

    private void _5_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "5";
        HandleCodeEntry(sender, e);
    }

    private void _6_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "6";
        HandleCodeEntry(sender, e);
    }

    private void _7_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "7";
        HandleCodeEntry(sender, e);
    }

    private void _8_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "8";
        HandleCodeEntry(sender, e);
    }

    private void _9_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "9";
        HandleCodeEntry(sender, e);
    }

    private void _0_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "0";
        HandleCodeEntry(sender, e);
    }
    private void star_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "*";
        HandleCodeEntry(sender, e);
    }

    private void hashtag_Click(object sender, RoutedEventArgs e)
    {
        _currentCode += "#";
        HandleCodeEntry(sender, e);
    }
    #endregion

    private void HandleCodeEntry(object sender, RoutedEventArgs e)
    {
        Label.Content = $"Code:\n{_currentCode}";

        if (_currentCode.Length < CODERED.Length && _currentCode.Length < CODEBLUE.Length) return;

        new Timer(temp =>
        {
            CheckCode(sender, e);
        }, null, 1000, 0);
    }

    private void CheckCode(object sender, RoutedEventArgs e)
    {
        if (_currentCode == CODERED)
        {
            this.Dispatcher.Invoke(() =>
            {
                Label.Content = "Correct code for team Red!\nStart Capturing!";

                switch (capturedTeam)
                {
                    case 0:
                        ProgressBar.Background = new SolidColorBrush(Color.FromRgb(230, 230, 230));
                        break;
                    case 1:
                        ProgressBar.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        break;
                    case 2:
                        ProgressBar.Background = new SolidColorBrush(Color.FromRgb(15, 159, 242));
                        break;
                }

                ProgressBar.Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                currentTeam = 1;

                if (currentTeam != capturedTeam) capturedAmount = 1;
                _currentCode = "";

                ProgressBar.Value = capturedAmount;


            });
        }
        else if (_currentCode == CODEBLUE)
        {
            this.Dispatcher.Invoke(() =>
            {
                Label.Content = "Correct code for team Blue!\nStart Capturing!";

                switch (capturedTeam)
                {
                    case 0:
                        ProgressBar.Background = new SolidColorBrush(Color.FromRgb(230, 230, 230));
                        break;
                    case 1:
                        ProgressBar.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                        break;
                    case 2:
                        ProgressBar.Background = new SolidColorBrush(Color.FromRgb(15, 159, 242));
                        break;
                }

                ProgressBar.Foreground = new SolidColorBrush(Color.FromRgb(15, 159, 242));
                currentTeam = 2;
                if (currentTeam != capturedTeam) capturedAmount = 1;
                _currentCode = "";
                ProgressBar.Value = capturedAmount;


            });
        }
        else
        {
            _currentCode = "";
            this.Dispatcher.Invoke(() =>
            {
                Label.Content = "Wrong code! Try again!";
                currentTeam = 0;
            });
        }
    }

    Timer buttonTimer;

    private void Button_Mouse_Up(object sender, MouseButtonEventArgs e)
    {
        if (buttonTimer == null) return;

        buttonTimer.Dispose();

        if (capturedAmount < 100) capturedAmount = 1;
        ProgressBar.Value = capturedAmount;

    }

    bool newCapture = true;

    private void Button_Mouse_Down(object sender, MouseButtonEventArgs e)
    { 
        if (currentTeam == 0) return;
        if (currentTeam != capturedTeam)
        {
            capturedAmount = 1;
            newCapture = true;
        }
        buttonTimer = new Timer(e =>
        {
            if (capturedAmount < 100 && capturedAmount % 10 == 0)
            {
                State state = new State { capturePercentage = capturedAmount, capturer = currentTeam };
                string str = JsonConvert.SerializeObject(state);
                MqttApplicationMessage message = new MqttApplicationMessageBuilder().WithPayload(JsonConvert.SerializeObject(state))
                .WithTopic("gadget/01234567890A/state")
                .Build();
                client.Client.PublishAsync(message, CancellationToken.None);
                capturedAmount++;
            } else if (capturedAmount < 100)
            {
                capturedAmount++;
            }
            if (capturedAmount >= 100) this.Dispatcher.Invoke(() =>
            {
                capturedTeam = currentTeam;
                Label.Content = "Captured!";

                State state = new State { capturePercentage = 100, capturer = capturedTeam };
                
                MqttApplicationMessage message = new MqttApplicationMessageBuilder().WithPayload(JsonConvert.SerializeObject(state))
                .WithTopic("gadget/01234567890A/state")
                .Build();
                if (newCapture) client.Client.PublishAsync(message, CancellationToken.None);
                newCapture = false;
            });
            else this.Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = capturedAmount;
            });
        }, null, 0, 50);
    }
}