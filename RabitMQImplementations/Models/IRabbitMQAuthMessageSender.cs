namespace RabitMQImplementations.Models
{
	public interface IRabbitMQAuthMessageSender
	{
		
		void SendMessage(object message, string queueName);
	}
}
