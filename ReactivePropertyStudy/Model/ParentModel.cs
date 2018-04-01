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
            // TODO: Dispose

            // ObservableCollectionのColelctionChangedで
            Children.CollectionChanged += (sender, e) =>
                {
                    // Collectionが0件になったら、true
                    if (Children.Count == 0)
                    {
                        IsValid = new ReactiveProperty<bool>(true);
                        return;
                    }

                    // すべての子要素のIsValidがtrueか？をReactiveProperty化
                    IsValid = Children.Select(x => x.IsValid)
                        .CombineLatestValuesAreAllTrue()
                        .ToReactiveProperty();
                };
        }
    }
}