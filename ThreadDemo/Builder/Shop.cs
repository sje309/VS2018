/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.Builder
*文件名称：Shop
*创建标识：shuyizhi  2018/10/11 11:03:30
*文件描述: 指挥者角色
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.Builder
{
    /// <summary>
    /// 指挥者角色Director
    /// </summary>
    public class Shop
    {
        #region //Methods

        public void Construct(VehicleBuilder builder)
        {
            builder.BuildFrame();

            builder.BuildDoors();

            builder.BuildEngine();
            
            builder.BuildWheels();
        }

        #endregion
    }
}
