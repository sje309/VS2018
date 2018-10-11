/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Builder
*文件名称：MotorCycleBuilder
*创建标识：shuyizhi  2018/10/11 10:44:21
*文件描述: 具体建造者
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Builder
{
    /// <summary>
    /// 具体建造者 ConcreteBuilder1
    /// </summary>
    public class MotorCycleBuilder : VehicleBuilder
    {
        public override void BuildDoors()
        {
            vehicle["doors"] = "0";
        }

        public override void BuildEngine()
        {
            //throw new NotImplementedException();
            vehicle["engine"] = "500 cc";
        }

        public override void BuildFrame()
        {
            //throw new NotImplementedException();
            vehicle=new Vehicle("MotorCycle");
            vehicle["frame"] = "MotorCycle Frame";
        }

        public override void BuildWheels()
        {
            //throw new NotImplementedException();
            vehicle["doors"] = "0";
        }
    }
}
