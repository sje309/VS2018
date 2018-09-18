using NUnit.Framework;
using Redis;
/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：RedisTests
*文件名称：HashClassTests
*创建人：  shuyizhi
*创建时间：2018/9/13 10:01:53
*文件描述: Redis项目测试
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis.Tests
{
    [TestFixture()]
    public class HashClassTests
    {
        [Test()]
        public void TestHGETAndHSETTest()
        {
            HashClass.TestHGETAndHSET();
        }
    }
}