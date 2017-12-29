﻿using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

using Newtonsoft.Json.Linq;

using OpenQA.Selenium;

namespace SKBKontur.SeleniumTesting.Controls
{
    public class RadioGroup : ControlBase
    {
        public RadioGroup(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
        }

        [Obsolete]
        public void SelectItem(object v)
        {
            SelectItemById(v.ToString());
        }

        [Obsolete]
        public string GetSelectedItem()
        {
            return GetSelectedItemId();
        }

        public void SelectItemById([NotNull] string id)
        {
            ExecuteAction(element =>
                {
                    var items = GetReactProp<JArray>("items");
                    var index = items.ToList().FindIndex(x => ElementMatchToValue(id, x));
                    element.FindElements(By.CssSelector(string.Format("[data-comp-name='{0}']", "Radio"))).ElementAt(index).Click();
                }, string.Format("SelectItemById({0})", id));
        }

        private static bool ElementMatchToValue(object value, JToken x)
        {
            object actualValue = null;
            if(x is JArray)
            {
                if(x[0] is JValue)
                {
                    actualValue = ((JValue)x[0]).Value;
                }
            }
            else
            {
                if(x is JValue)
                {
                    actualValue = ((JValue)x).Value;
                }
            }
            if(actualValue == null)
            {
                return value == null;
            }
            return 
                actualValue.Equals(value) || 
                actualValue.ToString().Equals(value.ToString()) || 
                actualValue.ToString().ToLower().Equals(value.ToString().ToLower());
        }

        [CanBeNull]
        public string GetSelectedItemId()
        {
            return GetReactProp<string>("value");
        }

        [NotNull]
        public List<string> GetItems()
        {
            return GetReactProp<object[][]>("items").Select(x => x[0].ToString()).ToList();
        }
    }
}