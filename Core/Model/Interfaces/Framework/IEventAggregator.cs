using System;

namespace WaveTech.Dafuscator.Model.Interfaces.Framework
{
	public interface IListener<T>
	{
		void Handle(T message);
	}

	public interface IEventAggregator
	{
		// Sending messages
		void SendMessage<T>(T message);
		void SendMessage<T>() where T : new();

		// Explicit registration
		IEventAggregator AddListener<T>(Action<T> handler);
		IEventAggregator AddListener<T>(Action handler);

		IEventAggregator AddListener(Action<object> handler, Predicate<object> filter);
		IEventAggregator AddListener(Action handler, Predicate<object> filter);

		// Listeners that apply a filter before handling
		IEventAggregator AddListener<T>(Action<T> handler, Predicate<T> filter);
		IEventAggregator AddListener<T>(Action handler, Predicate<T> filter);

		IEventAggregator AddListener(object listener);
		IEventAggregator RemoveListener(object listener);
	}
}