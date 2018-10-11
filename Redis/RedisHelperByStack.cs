/*----------------------------------------------------------------
// Copyright (C) 2018 xx单位
// 版权所有。
//
// 文件名称：RedisHelperByStack
// 功能描述：Stack Exchange Redis操作类
// 参考： https://www.cnblogs.com/godbell/p/7476529.html
//
// 创建者：shuyizhi
// 创建时间: 2018/9/26 22:12:14
----------------------------------------------------------------*/

using System;
using System.Text;

namespace Redis
{
    public class RedisHelperByStack
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static readonly string ConnectionString = System.Configuration.
            ConfigurationManager.ConnectionStrings["RedisConnectionString"].ConnectionString;

        /// <summary>
        /// 锁对象
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        /// 连接对象
        /// </summary>
        private volatile StackExchange.Redis.IConnectionMultiplexer _connection;

        private StackExchange.Redis.IDatabase _db;

        public RedisHelperByStack()
        {
            _connection = StackExchange.Redis.ConnectionMultiplexer.Connect(ConnectionString);
            _db = GetDatabase();
        }

        /// <summary>
        /// 获取连接
        /// </summary>
        /// <returns></returns>
        protected StackExchange.Redis.IConnectionMultiplexer GetConnection()
        {
            if (null != _connection && _connection.IsConnected)
            {
                return _connection;
            }
            lock (_lock)
            {
                if (null != _connection && _connection.IsConnected)
                {
                    return _connection;
                }
                if (null != _connection)
                {
                    _connection.Dispose();
                }
                _connection = StackExchange.Redis.ConnectionMultiplexer.Connect(ConnectionString);
            }
            return _connection;
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public StackExchange.Redis.IDatabase GetDatabase( int? db = null )
        {
            return GetConnection().GetDatabase(db ?? -1);
        }

        public virtual void Set( string key, object data, int cacheTime )
        {
            if (null != data)
            {
                return;
            }
            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);
            _db.StringSet(key, entryBytes, expiresIn);
        }

        #region //序列化与反序列化

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected byte[] Serialize( object data )
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        protected virtual T Deserialize<T>( byte[] serializedObject )
        {
            if (null == serializedObject)
            {
                return default(T);
            }
            var json = Encoding.UTF8.GetString(serializedObject);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        #endregion //序列化与反序列化
    }
}