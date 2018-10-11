/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Builder
*文件名称：CarBuilder
*创建标识：shuyizhi  2018/10/11 10:51:58
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
    /// 具体建造者ConcreteBuilder2
    /// </summary>
    public class CarBuilder : VehicleBuilder
    {
        public override void BuildDoors()
        {
            //throw new NotImplementedException();
            vehicle["doors"] = "4";
        }

        public override void BuildEngine()
        {
            //throw new NotImplementedException();
            vehicle["engine"] = "2500 cc";
        }

        public override void BuildFrame()
        {
            //throw new NotImplementedException();
            vehicle=new Vehicle("Car");
            vehicle["frame"] = "Car Frame";
        }

        public override void BuildWheels()
        {
            //throw new NotImplementedException();
            vehicle["wheels"] = "4";
        }
    }
}
