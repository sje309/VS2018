/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Singleton
*文件名称：Singleton
*创建标识：shuyizhi  2018/10/10 13:59:17
*文件描述: 单例模式的实现
* 参考：https://www.cnblogs.com/zhili/p/SingletonPatterm.html
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Singleton
{
    /// <summary>
    /// 单例模式的实现
    /// </summary>
    public class Singleton
    {
        /// <summary>
        /// 定义一个静态变量来保存类的实例
        /// </summary>
        private static Singleton uniqueInstance;
        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object locker = new object();
        /// <summary>
        /// 定义私有构造函数，外界不能创建该类的实例
        /// </summary>
        private Singleton() { }

        #region //多线程下线程不安全
        /// <summary>
        /// 定义公有方法提供一个全局访问点,也可以定义共有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        //public static Singleton GetSingleton()
        //{
        //    /**如果类的实例不存在则创建，否则直接返回*/
        //    if (null == uniqueInstance)
        //    {
        //        uniqueInstance = new Singleton();
        //    }
        //    return uniqueInstance;
        //}
        #endregion

        #region //多线程安全加锁的方式，增加性能开销
        //public static Singleton GetSingleton()
        //{
        //    /**
        //     当第一个线程运行到这里，此时会对locker对象"加锁"
        //     当第二个线程运行到这里，首先检测到locker对象处于"加锁"状态，该线程就会挂起等待第一个线程解锁
        //     lock语句运行完之后(即线程运行完之后)会对该对象"解锁"   
        //     */
        //    lock (locker)
        //    {
        //        if (null == uniqueInstance)
        //        {
        //            uniqueInstance = new Singleton();
        //        }
        //    }
        //    return uniqueInstance;
        //}
        #endregion

        #region //多线程双锁方式
        public Singleton GetSingleton()
        {
            if (null == uniqueInstance)
            {
                lock (locker)
                {
                    if (null == uniqueInstance)
                    {
                        uniqueInstance = new Singleton();
                    }
                }
            }
            return uniqueInstance;
        }
        #endregion
    }
}
