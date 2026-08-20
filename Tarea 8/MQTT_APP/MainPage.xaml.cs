using System.Diagnostics;
using MQTTnet;
using System.Text;
using System.Text.Json;

namespace MQTT_APP
{
    public partial class MainPage : ContentPage
    {

        private IMqttClient mqttClient = new MqttClientFactory().CreateMqttClient();

        private string topicoSuscripcion;
        private string topicoPublicacion;

        private List<string> ultimosMensajes = new List<string>();

        

        public MainPage()
        {
            InitializeComponent();

#if WINDOWS
            topicoSuscripcion = "PC/COMANDOS";
            topicoPublicacion = "CELULAR/ESTADO";
            MensajeTexto.Completed += enviarMensaje;

           
#elif ANDROID
            topicoSuscripcion = "CELULAR/ESTADO";
            topicoPublicacion = "PC/COMANDOS";
            
#endif


        }

        private void limpiarHistorial(object sender, EventArgs e)
        {
            ultimosMensajes.Clear();
            UltimoMensaje.FormattedText = new FormattedString();
        }

        //añadir mensajes
        private void AgregarMensaje(string mensaje)
        {
            ultimosMensajes.Insert(0, $"{mensaje} ({DateTime.Now.ToString("HH:mm:ss")})");

            if (ultimosMensajes.Count > 3)
            {
                ultimosMensajes.RemoveAt(ultimosMensajes.Count - 1);
            }

            var formattedString = new FormattedString();

            for (int i = 0; i < ultimosMensajes.Count; i++)
            {
                string[] partes = ultimosMensajes[i].Split(" (");

                formattedString.Spans.Add(new Span
                {
                    Text = partes[0],
                    TextColor = Colors.White,
                    FontSize = 18
                });

                formattedString.Spans.Add(new Span
                {
                    Text = $"\n{partes[1].TrimEnd(')')}",
                    TextColor = Colors.Gray,
                    FontSize = 13
                });

                if (i < ultimosMensajes.Count - 1)
                {
                    formattedString.Spans.Add(new Span
                    {
                        Text = "\n\n",
                        FontSize = 10
                    });
                }
            }

            UltimoMensaje.FormattedText = formattedString;
        }

        private void changeButton()
        {
            if (BotonConectar.Text == "Conectar")
            {
                BotonConectar.Text = "Desconectar";
            } else
            {
                BotonConectar.Text = "Conectar";
            }
        }


        private void changeConnectText()
        {
            if (EstadoConexion.Text == "Desconectado")
            {
                EstadoConexion.Text = "Conectado";
                EstadoConexion.TextColor = Colors.Green;
            }
            else
            {
                EstadoConexion.Text = "Desconectado";
                EstadoConexion.TextColor = Colors.Red;
            }
        }

        //Crear mensaje MQTT
        private async Task messageFactory(string mensaje)
        {
            if (mqttClient.IsConnected)
            {
                var message = new MqttApplicationMessageBuilder()
                 .WithTopic(topicoPublicacion)
                 .WithPayload(mensaje)
                 .Build();

                await mqttClient.PublishAsync(message);
            }
            
        }

        private async void enviarMensaje(object sender, EventArgs e)
        {
            string texto = MensajeTexto.Text;

            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            foreach (char c in texto)
            {
                if (!char.IsLetterOrDigit(c) && !char.IsPunctuation(c) && !char.IsWhiteSpace(c))
                {
                    return;
                }
            }

            await messageFactory(texto);

            MensajeTexto.Text = string.Empty;
        }

        private async void FunctionConnect(object sender, EventArgs e)
        {
            //Conectar MQTT
            if (mqttClient == null || !mqttClient.IsConnected)
            {
                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer("bd75534bca5b4795bd7eb15fafbdb796.s1.eu.hivemq.cloud", 8883)
                    .WithCredentials("Admin", "Pacoenmanuel@01")
                    .WithTlsOptions(o => o.WithCertificateValidationHandler(_ => true))
                    .Build();


                //oir broker
                mqttClient.ApplicationMessageReceivedAsync += async args =>
                {
                    string mensaje = args.ApplicationMessage.ConvertPayloadToString();

                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        AgregarMensaje(mensaje);
                    });
                };

                await mqttClient.ConnectAsync(options);

                changeConnectText();
                changeButton();

                //cambio suscribcion por prueba
                await mqttClient.SubscribeAsync(topicoSuscripcion);


                //Desconectar MQTT
            } else if (mqttClient.IsConnected) { 

                changeButton();
                changeConnectText();

            mqttClient.DisconnectAsync();

            }
        }
        }


    }
