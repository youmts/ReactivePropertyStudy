using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Reactive.Bindings;
using ReactivePropertyStudy.Annotations;
using Xunit.Sdk;

namespace ReactivePropertyStudy.Model
{
    public class ChildModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [RegularExpression(@"\A[A-Z0-9]{4}\z", ErrorMessage = "半角英数字で入力してください")]
        [Required(ErrorMessage = "入力してください")]
        public ReactiveProperty<string> Name { get; } = new ReactiveProperty<string>();

        public ReactiveProperty<bool> IsValid { get; }

        public ChildModel()
        {
            Name = new ReactiveProperty<string>()
                .SetValidateAttribute(() => Name);

            IsValid = Name.ObserveHasErrors
                .Select(x => !x)
                .ToReactiveProperty();
        }
    }
}