/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：ThreadDemo.DIP
*文件名称：RiverWater
*创建人：  shuyizhi
*创建时间：2018/10/9 13:52:47
*文件描述:DIP：依赖倒置
*参考：https://mp.weixin.qq.com/s/An0quyYzD4Bsph-9KTzfVg
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadDemo.DIP
{
    ///// <summary>
    ///// 河水
    ///// </summary>
    //public class RiverWater
    //{
    //    public void GiveNutrition()
    //    {
    //        Console.WriteLine("我是河水，我给小鱼提供养分。");
    //    }
    //}
    ///// <summary>
    ///// 鱼
    ///// </summary>
    //public class Fish
    //{
    //    private RiverWater water;
    //    public void Live()
    //    {
    //        water = new RiverWater();
    //        water.GiveNutrition();
    //    }
    //}

    public abstract class Water
    {
        public abstract void GiveNutrition();
    }
    /// <summary>
    /// 河水
    /// </summary>
    public class RiverWater : Water
    {
        public override void GiveNutrition()
        {
            //throw new NotImplementedException();
            Console.WriteLine("河水-提供养分。");
        }
    }
    /// <summary>
    /// 井水
    /// </summary>
    public class WellWater : Water
    {
        public override void GiveNutrition()
        {
            Console.WriteLine("井水-提供养分。");
        }
    }
    public class LakeWater : Water
    {
        public override void GiveNutrition()
        {
            Console.WriteLine("湖水-提供养分。");
        }
    }
    #region //DI 依赖注入
    /**1、构造函数注入*/
    //public class Fish
    //{
    //    private Water water;
    //    //public Fish()
    //    //{
    //    //    water = new LakeWater();
    //    //}
    //    public Fish( Water water )
    //    {
    //        this.water = water;
    //    }
    //    public void Live()
    //    {
    //        Console.WriteLine("我的生活靠: ");
    //        water.GiveNutrition();
    //    }
    //}

    /**2、setter方式注入*/
    //public class Fish
    //{
    //    private Water water;
    //    public Fish()
    //    {

    //    }
    //    public void setWater(Water water )
    //    {
    //        this.water = water;
    //    }
    //    public void Live()
    //    {
    //        if (null != water)
    //        {
    //            Console.WriteLine("我的生活靠：");
    //            water.GiveNutrition();
    //        }
    //    }
    //}

    /**3、接口注入*/
    public interface ISetWater
    {
        void setWater(Water water);
    }
    public class Fish : ISetWater
    {
        private Water water;
        public Fish()
        {

        }

        public void setWater( Water water )
        {
            this.water = water;
        }
        public void Live()
        {
            if (null != water)
            {
                Console.WriteLine("我的生活靠：");
                water.GiveNutrition();
            }
        }
    }
    #endregion

}
