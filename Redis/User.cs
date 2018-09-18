/************************************************************************
*Copyright (c) 2018 中新网安 All Rights Reserved.
*命名空间：Redis
*文件名称：User
*创建人：  shuyizhi
*创建时间：2018/9/13 13:46:05
*文件描述: 
************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redis
{
    [Serializable]
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Birth { get; set; }
       
        /// <summary>
        /// 生成10W条数据
        /// </summary>
        /// <returns></returns>
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            for(int i = 0; i < 100000; i++)
            {
                User u = new User();
                u.Address = "安徽省合肥市高新区望江西路与创新大道交叉口" + i;
                u.Birth = DateTime.Now.Subtract(new TimeSpan(0, 0, i));
                u.Id = Guid.NewGuid().ToString();
                u.Name = Guid.NewGuid().ToString().Replace("-", "");
                
                users.Add(u);
            }
            return users;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
