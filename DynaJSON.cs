﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Dynamic;

namespace HastyAPI {
	public class DynaJSON {
		public static dynamic Parse(string text) {
			var js = new JavaScriptSerializer().DeserializeObject(text) as Dictionary<string, object>;

			var result = GetObject(js as Dictionary<string, object>);

			return result;
		}

		private static dynamic GetObject(Dictionary<string, object> dic) {
			var obj = new ExpandoObject() as IDictionary<string, object>;
			foreach(var pair in dic) {
				obj.Add(pair.Key.Replace('-', '_'), GetValue(pair.Value));
			}
			return obj;
		}

		private static object GetValue(object val) {
			if(val is object[]) {
				return GetList(val as object[]);
			} else if(val is Dictionary<string, object>) {
				return GetObject(val as Dictionary<string, object>);
			}
			return val; // primitive
		}

		private static dynamic GetList(object[] ary) {
			var list = new List<object>(ary.Length);
			foreach(var e in ary) {
				list.Add(GetValue(e));
			}
			return list;
		}
	}
}
