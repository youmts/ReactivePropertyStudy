using ReactivePropertyStudy.Model;

namespace Xunit
{
    public class ChildModelTest
    {
        [Fact]
        public void NameTest()
        {
            var model = new ChildModel();
            model.Name.Value = "AA00";
            model.IsValid.Value.IsTrue();
        }

        [Fact]
        public void NameTest2()
        {
            var model = new ChildModel();
            model.Name.Value = "AAああ";
            model.IsValid.Value.IsFalse();
        }

        [Fact]
        public void NameTest3()
        {
            var model = new ChildModel();
            model.Name.Value = null;
            model.IsValid.Value.IsFalse();
        }
    }
}