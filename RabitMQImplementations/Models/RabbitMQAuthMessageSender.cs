using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RabbitMQ.Client;
using System;
using System.Text;

namespace RabitMQImplementations.Models
{
	public class RabbitMQAuthMessageSender : IRabbitMQAuthMessageSender
	{
		private readonly string _hostName;
		private readonly string _userName;
		private readonly string _password;
        public RabbitMQAuthMessageSender()
        {
			_hostName = "localhost";
			_userName = "guest";
			_password = "guest";
		}
        public void SendMessage(object message, string queueName)
		{
			var factory = new ConnectionFactory
			{
				HostName = _hostName,
				UserName = _userName,
				Password = _password
			};
			var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			channel.QueueDeclare(queueName,false,false,false,null);
			string json = JsonConvert.SerializeObject(message, new VersionConverter());
			var body = Encoding.UTF8.GetBytes(json);
			channel.BasicPublish(exchange:"",routingKey:queueName,basicProperties:null, body:body);
		}
	}
}
