/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Builder
*文件名称：Vehicle
*创建标识：shuyizhi  2018/10/11 10:28:11
*文件描述: 产品角色
************************************************************************/


using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Builder
{
    /// <summary>
    /// products
    /// </summary>
    public class Vehicle
    {
        #region //Fields

        private string type;
        private Hashtable parts=new Hashtable();

        #endregion

        #region //Constructs

        public Vehicle(string type)
        {
            this.type = type;
        }
        #endregion

        #region //Indexers

        public object this[string key]
        {
            get { return parts[key]; }
            set { parts[key] = value; }
        }
        #endregion

        #region //Methods

        public void show()
        {
            Console.WriteLine("---------------------------");
            Console.WriteLine("Vehicle Type: "+type);
            Console.WriteLine("Frame: "+parts["frame"]);
            Console.WriteLine("Engine: "+parts["engine"]);
            Console.WriteLine("#Wheels: "+parts["wheels"]);
            Console.WriteLine("#Doors: "+parts["doors"]);
        }
        #endregion
    }
}
