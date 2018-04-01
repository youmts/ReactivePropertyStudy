using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using ReactivePropertyStudy.Annotations;

namespace ReactivePropertyStudy.Model
{
    public class ParentModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ChildModel> Children { get; } = new ObservableCollection<ChildModel>();

        public ReactiveProperty<bool> IsValid { get; private set; } = new ReactiveProperty<bool>(true);

        public ParentModel()
        {
            Children.CollectionChanged += (sender, e) =>
                {
                    if (Children.Count == 0)
                    {
                        IsValid = new ReactiveProperty<bool>(true);
                        return;
                    }

                    IsValid = Children.Select(x => x.IsValid)
                        .CombineLatestValuesAreAllTrue()
                        .ToReactiveProperty();
                };
        }
    }
}