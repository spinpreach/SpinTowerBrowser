using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spinpreach.ITowerBase
{

    public enum APIS
    {

        /// <summary></summary>
        login,

        /// <summary></summary>
        imagelist,

        /// <summary></summary>
        soundlist,

        /// <summary></summary>
        constant,

        /// <summary></summary>
        homeinit,

        /// <summary></summary>
        batheinit,

        /// <summary>休憩所を覗いた時</summary>
        restinit,

        /// <summary></summary>
        cardeditinit,

        /// <summary></summary>
        cardinfo,

        /// <summary></summary>
        searchinit,

        /// <summary></summary>
        shopinit,

        /// <summary></summary>
        iteminit,

        /// <summary></summary>
        infoinit,

        /// <summary></summary>
        ranking,

        /// <summary></summary>
        bookinit,

        /// <summary>任務一覧</summary>
        reqinit,

        /// <summary>罠設置時</summary>
        trapset,

        /// <summary>任務達成時</summary>
        reqget,

        /// <summary>休憩時</summary>
        restset,

        /// <summary>編成変更時</summary>
        cardchange,

        /// <summary>探索時</summary>
        searchnext,

        /// <summary>班解散時</summary>
        groupexit,

        /// <summary>建機移籍時</summary>
        cardmultidel

    }

    public static class ApisExtension
    {

        public static string Name(this APIS value)
        {
            var values = List().ToArray();
            return values[(int)value];
        }

        public static APIS Value(String Name)
        {
            foreach (var item in Enum.GetValues(typeof(APIS)))
            {
                if (item.ToString() == Name)
                {
                    return (APIS)item;
                }
            }
            throw new Exception("指定された [Name] が [APIS] に存在しません。");
        }

        public static List<string> List()
        {
            var list = new List<string>();
            foreach (var item in Enum.GetValues(typeof(APIS)))
            {
                list.Add(item.ToString());
            }
            return list;
        }

    }
}