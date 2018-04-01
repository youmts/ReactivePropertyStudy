using System;
using System.Data;
using System.Diagnostics;
using ReactivePropertyStudy.Model;

namespace Xunit
{
    public class ParentModelTest
    {
        private ParentModel _model;

        private ChildModel AddChild(string name)
        {
            ChildModel child; 
            _model.Children.Add(child = new ChildModel()
            {
                Name = { Value = name }
            });

            return child;
        }

        public ParentModelTest()
        {
            Debug.Listeners.Add(new DefaultTraceListener());
            _model = new ParentModel();
        }

        [Fact]
        public void Initial()
        {
            _model.IsValid.Value.IsTrue();
        }

        [Fact]
        public void AddValidChild()
        {
            _model.Children.Add(new ChildModel()
            {
                Name = { Value = "AAA0" }
            });
            _model.IsValid.Value.IsTrue();
        }

        [Fact]
        public void AddInvalidChild()
        {
            _model.Children.Add(new ChildModel()
            {
                Name = { Value = null }
            });
            _model.IsValid.Value.IsFalse();
        }

        [Fact]
        public void AddValidChildThenRemove()
        {
            _model.Children.Add(new ChildModel()
            {
                Name = { Value = "AAA0" }
            });
            _model.Children.RemoveAt(0);
            _model.IsValid.Value.IsTrue();
        }

        [Fact]
        public void AddInvalidChildThenRemove()
        {
            _model.Children.Add(new ChildModel()
            {
                Name = { Value = null }
            });
            _model.Children.RemoveAt(0);
            _model.IsValid.Value.IsTrue();
        }

        [Theory]
        [InlineData("AAA0", "    ", "    ")]
        [InlineData("    ", "AAA0", "    ")]
        [InlineData("    ", "    ", "AAA0")]
        [InlineData("AAA0", "AAA0", "    ")]
        [InlineData("    ", "AAA0", "AAA0")]
        [InlineData("AAA0", "    ", "AAA0")]
        public void AnyFalseTest(string a, string b, string c)
        {
            foreach (var s in new []{a, b, c})
            {
                _model.Children.Add(new ChildModel()
                {
                    Name = { Value = s }
                });
            }

            _model.IsValid.Value.IsFalse();
        }

        [Fact]
        public void AllTrueTest()
        {
            foreach (var s in new[] { "AAAA", "BBBB", "CCCC" })
            {
                _model.Children.Add(new ChildModel()
                {
                    Name = { Value = s }
                });
            }

            _model.IsValid.Value.IsTrue();
        }

        [Fact]
        public void AddRemoveTest()
        {
            _model.IsValid.Value.IsTrue();
            var a = AddChild("AAAA"); _model.IsValid.Value.IsTrue();
            var b = AddChild("    "); _model.IsValid.Value.IsFalse();
            _model.Children.Remove(b);
            _model.IsValid.Value.IsTrue();
        }

        [Fact]
        public void ChildValueChangeTest()
        {
            Console.WriteLine("start");
            Debug.WriteLine("start");
            var child = AddChild("AAAA"); _model.IsValid.Value.IsTrue();
            child.Name.Value = "    "; _model.IsValid.Value.IsFalse();
            child.Name.Value = "AAAA";
            _model.IsValid.Value.IsTrue();
        }
    }
}