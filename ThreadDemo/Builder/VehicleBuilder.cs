/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Builder
*文件名称：VehicleBuilder
*创建标识：shuyizhi  2018/10/11 10:35:43
*文件描述: 抽象建造者
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Builder
{
    /// <summary>
    /// 抽象建造者Builder
    /// </summary>
    public abstract class VehicleBuilder
    {
        #region //Fileds

        protected Vehicle vehicle = null;

        #endregion

        #region //Properties

        public Vehicle Vehicle
        {
            get { return vehicle; }
        }

        #endregion

        #region //Methods

        public abstract void BuildFrame();
        public abstract void BuildEngine();
        public abstract void BuildWheels();
        public abstract void BuildDoors();

        #endregion
    }
}
