﻿namespace SKBKontur.SeleniumTesting.Controls
{
    public class Checkbox : ControlBase
    {
        public Checkbox(ISearchContainer container, ISelector selector)
            : base(container, selector)
        {
            Label = new Label(this, new UniversalSelector("*:first-child + * + *"));
        }

        public Label Label { get; private set; }

        public IControlProperty<bool> IsChecked => ReactProperty<bool>("checked");
        public IControlProperty<bool> IsDisabled => ReactProperty<bool>("disabled");
    }
}