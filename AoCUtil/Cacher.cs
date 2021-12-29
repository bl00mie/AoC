using System;
using System.Collections.Generic;

namespace AoC
{
	public static class Cacher
	{
		public static Cacher<TIn, TOut> Wrap<TIn, TOut>(Func<TIn, TOut> method) => new Cacher<TIn, TOut>(method);
		static public CustomKeyCacher<TIn, TOut, TKey> Wrap<TIn, TOut, TKey>(Func<TIn, TOut> method, Func<TIn, TKey> keyMaker) => new CustomKeyCacher<TIn, TOut, TKey>(method, keyMaker);

		public static Cacher<TIn1, TIn2, TOut> Wrap<TIn1, TIn2, TOut>(Func<TIn1, TIn2, TOut> method) => new Cacher<TIn1, TIn2, TOut>(method);
		static public CustomKeyCacher<TIn1, TIn2, TOut, TKey> Wrap<TIn1, TIn2, TOut, TKey>(Func<TIn1, TIn2, TOut> method, Func<TIn1, TIn2, TKey> keyMaker) => new CustomKeyCacher<TIn1, TIn2, TOut, TKey>(method, keyMaker);

		public static Cacher<TIn1, TIn2, TIn3, TOut> Wrap<TIn1, TIn2, TIn3, TOut>(Func<TIn1, TIn2, TIn3, TOut> method) => new Cacher<TIn1, TIn2, TIn3, TOut>(method);
		static public CustomKeyCacher<TIn1, TIn2, TIn3, TOut, TKey> Wrap<TIn1, TIn2, TIn3, TOut, TKey>(Func<TIn1, TIn2, TIn3, TOut> method, Func<TIn1, TIn2, TIn3, TKey> keyMaker) => new CustomKeyCacher<TIn1, TIn2, TIn3, TOut, TKey>(method, keyMaker);

		public static Cacher<TIn1, TIn2, TIn3, TIn4, TOut> Wrap<TIn1, TIn2, TIn3, TIn4, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TOut> method) => new Cacher<TIn1, TIn2, TIn3, TIn4, TOut>(method);
		static public CustomKeyCacher<TIn1, TIn2, TIn3, TIn4, TOut, TKey> Wrap<TIn1, TIn2, TIn3, TIn4, TOut, TKey>(Func<TIn1, TIn2, TIn3, TIn4, TOut> method, Func<TIn1, TIn2, TIn3, TIn4, TKey> keyMaker) => new CustomKeyCacher<TIn1, TIn2, TIn3, TIn4, TOut, TKey>(method, keyMaker);

		public static Cacher<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> Wrap<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> method) => new Cacher<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>(method);
		static public CustomKeyCacher<TIn1, TIn2, TIn3, TIn4, TIn5, TOut, TKey> Wrap<TIn1, TIn2, TIn3, TIn4, TIn5, TOut, TKey>(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> method, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TKey> keyMaker) => new CustomKeyCacher<TIn1, TIn2, TIn3, TIn4, TIn5, TOut, TKey>(method, keyMaker);
	}

	public class Cacher<TIn, TOut>
    {
		private Func<TIn, TOut> _method;
		private Dictionary<TIn, TOut> _cache = new();

		public Cacher(Func<TIn, TOut> method) => _method = method;

		public TOut Invoke(TIn @in)
		{
			if (!_cache.TryGetValue(@in, out var result))
			{
				result = _method(@in);
				_cache[@in] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class CustomKeyCacher<TIn, TOut, TKey>
    {
		private Func<TIn, TKey> _keyMaker;
		private Func<TIn, TOut> _method;
		private Dictionary<TKey, TOut> _cache = new();

		public CustomKeyCacher(Func<TIn, TOut> method, Func<TIn, TKey> keyMaker)
        {
			_method = method;
			_keyMaker = keyMaker;
        }

		public TOut Invoke(TIn @in)
        {
			var key = _keyMaker(@in);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(@in);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class CustomKeyCacher<TIn1, TIn2, TOut, TKey>
	{
		Func<TIn1, TIn2, TKey> _keyMaker;
		Func<TIn1, TIn2, TOut> _method;
		Dictionary<TKey, TOut> _cache = new();

		public CustomKeyCacher(Func<TIn1, TIn2, TOut> method, Func<TIn1, TIn2, TKey> keyMaker)
		{
			_method = method;
			_keyMaker = keyMaker;
		}

		public TOut Invoke(TIn1 in1, TIn2 in2)
		{
			var key = _keyMaker(in1, in2);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class Cacher<TIn1, TIn2, TOut>
	{
		Func<TIn1, TIn2, TOut> _method;
		Dictionary<(TIn1, TIn2), TOut> _cache = new();

		public Cacher(Func<TIn1, TIn2, TOut> method) => _method = method;

		public TOut Invoke(TIn1 in1, TIn2 in2)
		{
			var key = (in1, in2);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class CustomKeyCacher<TIn1, TIn2, TIn3, TOut, TKey>
	{
		Func<TIn1, TIn2, TIn3, TKey> _keyMaker;
		Func<TIn1, TIn2, TIn3, TOut> _method;
		Dictionary<TKey, TOut> _cache = new();

		public CustomKeyCacher(Func<TIn1, TIn2, TIn3, TOut> method, Func<TIn1, TIn2, TIn3, TKey> keyMaker)
		{
			_method = method;
			_keyMaker = keyMaker;
		}

		public TOut Invoke(TIn1 in1, TIn2 in2, TIn3 in3)
		{
			var key = _keyMaker(in1, in2, in3);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2, in3);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class Cacher<TIn1, TIn2, TIn3, TOut>
	{
		Func<TIn1, TIn2, TIn3, TOut> _method;
		Dictionary<(TIn1, TIn2, TIn3), TOut> _cache = new();

		public Cacher(Func<TIn1, TIn2, TIn3, TOut> method) => _method = method;

		public TOut Invoke(TIn1 in1, TIn2 in2, TIn3 in3)
		{
			var key = (in1, in2, in3);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2, in3);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class CustomKeyCacher<TIn1, TIn2, TIn3, TIn4, TOut, TKey>
	{
		Func<TIn1, TIn2, TIn3, TIn4, TKey> _keyMaker;
		Func<TIn1, TIn2, TIn3, TIn4, TOut> _method;
		Dictionary<TKey, TOut> _cache = new();

		public CustomKeyCacher(Func<TIn1, TIn2, TIn3, TIn4, TOut> method, Func<TIn1, TIn2, TIn3, TIn4, TKey> keyMaker)
		{
			_method = method;
			_keyMaker = keyMaker;
		}

		public TOut Invoke(TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4)
		{
			var key = _keyMaker(in1, in2, in3, in4);
			if (!_cache.TryGetValue(key, out var result))
            {
				result = _method(in1, in2, in3, in4);
				_cache[key] = result;
            }				
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class Cacher<TIn1, TIn2, TIn3, TIn4, TOut>
	{
		Func<TIn1, TIn2, TIn3, TIn4, TOut> _method;
		Dictionary<(TIn1, TIn2, TIn3, TIn4), TOut> _cache = new();

		public Cacher(Func<TIn1, TIn2, TIn3, TIn4, TOut> method) => _method = method;

		public TOut Invoke(TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4)
		{
			var key = (in1, in2, in3, in4);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2, in3, in4);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class CustomKeyCacher<TIn1, TIn2, TIn3, TIn4, TIn5, TOut, TKey>
	{
		Func<TIn1, TIn2, TIn3, TIn4, TIn5, TKey> _keyMaker;
		Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> _method;
		Dictionary<TKey, TOut> _cache = new();

		public CustomKeyCacher(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> method, Func<TIn1, TIn2, TIn3, TIn4, TIn5, TKey> keyMaker)
		{
			_method = method;
			_keyMaker = keyMaker;
		}

		public TOut Invoke(TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5)
		{
			var key = _keyMaker(in1, in2, in3, in4, in5);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2, in3, in4, in5);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}

	public class Cacher<TIn1, TIn2, TIn3, TIn4, TIn5, TOut>
	{
		Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> _method;
		Dictionary<(TIn1, TIn2, TIn3, TIn4, TIn5), TOut> _cache = new();

		public Cacher(Func<TIn1, TIn2, TIn3, TIn4, TIn5, TOut> method) => _method = method;

		public TOut Invoke(TIn1 in1, TIn2 in2, TIn3 in3, TIn4 in4, TIn5 in5 )
		{
			var key = (in1, in2, in3, in4, in5);
			if (!_cache.TryGetValue(key, out var result))
			{
				result = _method(in1, in2, in3, in4, in5);
				_cache[key] = result;
			}
			return result;
		}

		public void Clear() => _cache.Clear();
	}
}
