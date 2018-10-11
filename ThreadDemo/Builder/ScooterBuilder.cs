/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Builder
*文件名称：ScooterBuilder
*创建标识：shuyizhi  2018/10/11 11:02:38
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Builder
{
    // "ConcreteBuilder3"
    public class ScooterBuilder : VehicleBuilder
    {
        // Methods
        override public void BuildFrame()
        {
            vehicle = new Vehicle("Scooter");
            vehicle["frame"] = "Scooter Frame";
        }
        override public void BuildEngine()
        {
            vehicle["engine"] = "none";
        }
        override public void BuildWheels()
        {
            vehicle["wheels"] = "2";
        }
        override public void BuildDoors()
        {
            vehicle["doors"] = "0";
        }
    }
}
