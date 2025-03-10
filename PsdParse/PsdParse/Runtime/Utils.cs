using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsdParse
{
    public class Utils
    {
        /// <summary>
        /// 向上取因数的倍数
        /// </summary>
        /// <param name="value">初始值</param>
        /// <param name="factor">因数</param>
        /// <returns></returns>
        public static uint RoundUp(uint value, uint factor)
        {
            return (value / factor + (value % factor > 0 ? 1u : 0)) * factor;
        }
    }
}
