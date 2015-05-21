using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace Common
{
    public class SessionHelper
    {
        
       public   RedisHelper Redis = new RedisHelper(true);
        public object this[string key]
        {
            get 
            {
                key = CookieHelper.CreatSessionCookie() + "_" + key;
                
                //距离过期时间还有多少秒
                long l = Redis.TTL(key);
                return Redis.Get<object>(key); 
            }
            set 
            {
                SetSession(key,value);
            }
        }
        public   void SetSession(string key,object value)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new Exception("Key is Null or Epmty");
            }
            key = CookieHelper.CreatSessionCookie() + "_"+key;
            Redis.Set<object>(key, value,60*3);
        }
        public string SessionId
        {
            get { return  CookieHelper.CreatSessionCookie();}
        }
    }
}
