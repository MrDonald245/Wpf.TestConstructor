using System;
using System.Collections.ObjectModel;

namespace Wpf.TestBuilder.Models
{
    [Serializable]
    public class OptionsModel : ObservableCollection<OptionModel>
    {
        /// <summary>
        /// Before removing an item make sure except one answer is right.
        /// </summary>
        /// <param name="index"></param>
        protected override void RemoveItem(int index)
        {
            if (this[index].IsRightAnswer)
            {
                base.RemoveItem(index);

                if (Count != 0)
                    this[0].IsRightAnswer = true;

                return;
            }

            base.RemoveItem(index);
        }
    }
}