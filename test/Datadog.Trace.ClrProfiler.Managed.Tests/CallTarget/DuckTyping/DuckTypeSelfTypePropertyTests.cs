using System.Collections.Generic;
using Datadog.Trace.ClrProfiler.CallTarget.DuckTyping;
using Xunit;
using static Datadog.Trace.ClrProfiler.Managed.Tests.CallTarget.DuckTyping.DuckTypeSelfTypeFieldTests;

#pragma warning disable SA1201 // Elements must appear in the correct order

namespace Datadog.Trace.ClrProfiler.Managed.Tests.CallTarget.DuckTyping
{
    public class DuckTypeSelfTypePropertyTests
    {
        public static IEnumerable<object[]> Data()
        {
            return new[]
            {
                new object[] { ObscureObject.GetPropertyPublicObject() },
                new object[] { ObscureObject.GetPropertyInternalObject() },
                new object[] { ObscureObject.GetPropertyPrivateObject() },
            };
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void StaticPropertyOnlySetException(object obscureObject)
        {
            Assert.Throws<DuckTypePropertyCantBeWrittenException>(() =>
            {
                obscureObject.As<IObscureStaticErrorDuckType>();
            });
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void PropertyOnlySetException(object obscureObject)
        {
            Assert.Throws<DuckTypePropertyCantBeWrittenException>(() =>
            {
                obscureObject.As<IObscureErrorDuckType>();
            });
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void StaticOnlyGetProperties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            // *

            Assert.Equal(42, duckInterface.PublicStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.PublicStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.PublicStaticGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.PublicStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.PublicStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.PublicStaticGetSelfType).Instance);

            // *

            Assert.Equal(42, duckInterface.InternalStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.InternalStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.InternalStaticGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.InternalStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.InternalStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.InternalStaticGetSelfType).Instance);

            // *

            Assert.Equal(42, duckInterface.ProtectedStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.ProtectedStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.ProtectedStaticGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.ProtectedStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.ProtectedStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.ProtectedStaticGetSelfType).Instance);

            // *

            Assert.Equal(42, duckInterface.PrivateStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.PrivateStaticGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.PrivateStaticGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.PrivateStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.PrivateStaticGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.PrivateStaticGetSelfType).Instance);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void StaticProperties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            IDummyFieldObject newDummy = null;

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 42 }).As<IDummyFieldObject>();
            duckInterface.PublicStaticGetSetSelfType = newDummy;

            Assert.Equal(42, duckInterface.PublicStaticGetSetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.PublicStaticGetSetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.PublicStaticGetSetSelfType.MagicNumber);

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 52 }).As<IDummyFieldObject>();
            duckInterface.InternalStaticGetSetSelfType = newDummy;

            Assert.Equal(52, duckInterface.InternalStaticGetSetSelfType.MagicNumber);
            Assert.Equal(52, duckAbstract.InternalStaticGetSetSelfType.MagicNumber);
            Assert.Equal(52, duckVirtual.InternalStaticGetSetSelfType.MagicNumber);

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 62 }).As<IDummyFieldObject>();
            duckAbstract.ProtectedStaticGetSetSelfType = newDummy;

            Assert.Equal(62, duckInterface.ProtectedStaticGetSetSelfType.MagicNumber);
            Assert.Equal(62, duckAbstract.ProtectedStaticGetSetSelfType.MagicNumber);
            Assert.Equal(62, duckVirtual.ProtectedStaticGetSetSelfType.MagicNumber);

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 72 }).As<IDummyFieldObject>();
            duckAbstract.PrivateStaticGetSetSelfType = newDummy;

            Assert.Equal(72, duckInterface.PrivateStaticGetSetSelfType.MagicNumber);
            Assert.Equal(72, duckAbstract.PrivateStaticGetSetSelfType.MagicNumber);
            Assert.Equal(72, duckVirtual.PrivateStaticGetSetSelfType.MagicNumber);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void OnlyGetProperties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            // *

            Assert.Equal(42, duckInterface.PublicGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.PublicGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.PublicGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.PublicGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.PublicGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.PublicGetSelfType).Instance);

            // *

            Assert.Equal(42, duckInterface.InternalGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.InternalGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.InternalGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.InternalGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.InternalGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.InternalGetSelfType).Instance);

            // *

            Assert.Equal(42, duckInterface.ProtectedGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.ProtectedGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.ProtectedGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.ProtectedGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.ProtectedGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.ProtectedGetSelfType).Instance);

            // *

            Assert.Equal(42, duckInterface.PrivateGetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.PrivateGetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.PrivateGetSelfType.MagicNumber);

            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckInterface.PrivateGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckAbstract.PrivateGetSelfType).Instance);
            Assert.Equal(ObscureObject.DummyFieldObject.Default, ((IDuckType)duckVirtual.PrivateGetSelfType).Instance);
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Properties(object obscureObject)
        {
            var duckInterface = obscureObject.As<IObscureDuckType>();
            var duckAbstract = obscureObject.As<ObscureDuckTypeAbstractClass>();
            var duckVirtual = obscureObject.As<ObscureDuckType>();

            IDummyFieldObject newDummy = null;

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 42 }).As<IDummyFieldObject>();
            duckInterface.PublicGetSetSelfType = newDummy;

            Assert.Equal(42, duckInterface.PublicGetSetSelfType.MagicNumber);
            Assert.Equal(42, duckAbstract.PublicGetSetSelfType.MagicNumber);
            Assert.Equal(42, duckVirtual.PublicGetSetSelfType.MagicNumber);

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 52 }).As<IDummyFieldObject>();
            duckInterface.InternalGetSetSelfType = newDummy;

            Assert.Equal(52, duckInterface.InternalGetSetSelfType.MagicNumber);
            Assert.Equal(52, duckAbstract.InternalGetSetSelfType.MagicNumber);
            Assert.Equal(52, duckVirtual.InternalGetSetSelfType.MagicNumber);

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 62 }).As<IDummyFieldObject>();
            duckInterface.ProtectedGetSetSelfType = newDummy;

            Assert.Equal(62, duckInterface.ProtectedGetSetSelfType.MagicNumber);
            Assert.Equal(62, duckAbstract.ProtectedGetSetSelfType.MagicNumber);
            Assert.Equal(62, duckVirtual.ProtectedGetSetSelfType.MagicNumber);

            // *
            newDummy = (new ObscureObject.DummyFieldObject { MagicNumber = 72 }).As<IDummyFieldObject>();
            duckInterface.PrivateGetSetSelfType = newDummy;

            Assert.Equal(72, duckInterface.PrivateGetSetSelfType.MagicNumber);
            Assert.Equal(72, duckAbstract.PrivateGetSetSelfType.MagicNumber);
            Assert.Equal(72, duckVirtual.PrivateGetSetSelfType.MagicNumber);
        }

        public interface IObscureDuckType
        {
            IDummyFieldObject PublicStaticGetSelfType { get; }

            IDummyFieldObject InternalStaticGetSelfType { get; }

            IDummyFieldObject ProtectedStaticGetSelfType { get; }

            IDummyFieldObject PrivateStaticGetSelfType { get; }

            // *

            IDummyFieldObject PublicStaticGetSetSelfType { get; set; }

            IDummyFieldObject InternalStaticGetSetSelfType { get; set; }

            IDummyFieldObject ProtectedStaticGetSetSelfType { get; set; }

            IDummyFieldObject PrivateStaticGetSetSelfType { get; set; }

            // *

            IDummyFieldObject PublicGetSelfType { get; }

            IDummyFieldObject InternalGetSelfType { get; }

            IDummyFieldObject ProtectedGetSelfType { get; }

            IDummyFieldObject PrivateGetSelfType { get; }

            // *

            IDummyFieldObject PublicGetSetSelfType { get; set; }

            IDummyFieldObject InternalGetSetSelfType { get; set; }

            IDummyFieldObject ProtectedGetSetSelfType { get; set; }

            IDummyFieldObject PrivateGetSetSelfType { get; set; }
        }

        public interface IObscureStaticErrorDuckType
        {
            IDummyFieldObject PublicStaticGetSelfType { get; set; }
        }

        public interface IObscureErrorDuckType
        {
            IDummyFieldObject PublicGetSelfType { get; set; }
        }

        public abstract class ObscureDuckTypeAbstractClass
        {
            public abstract IDummyFieldObject PublicStaticGetSelfType { get; }

            public abstract IDummyFieldObject InternalStaticGetSelfType { get; }

            public abstract IDummyFieldObject ProtectedStaticGetSelfType { get; }

            public abstract IDummyFieldObject PrivateStaticGetSelfType { get; }

            // *

            public abstract IDummyFieldObject PublicStaticGetSetSelfType { get; set; }

            public abstract IDummyFieldObject InternalStaticGetSetSelfType { get; set; }

            public abstract IDummyFieldObject ProtectedStaticGetSetSelfType { get; set; }

            public abstract IDummyFieldObject PrivateStaticGetSetSelfType { get; set; }

            // *

            public abstract IDummyFieldObject PublicGetSelfType { get; }

            public abstract IDummyFieldObject InternalGetSelfType { get; }

            public abstract IDummyFieldObject ProtectedGetSelfType { get; }

            public abstract IDummyFieldObject PrivateGetSelfType { get; }

            // *

            public abstract IDummyFieldObject PublicGetSetSelfType { get; set; }

            public abstract IDummyFieldObject InternalGetSetSelfType { get; set; }

            public abstract IDummyFieldObject ProtectedGetSetSelfType { get; set; }

            public abstract IDummyFieldObject PrivateGetSetSelfType { get; set; }
        }

        public class ObscureDuckType
        {
            public virtual IDummyFieldObject PublicStaticGetSelfType { get; }

            public virtual IDummyFieldObject InternalStaticGetSelfType { get; }

            public virtual IDummyFieldObject ProtectedStaticGetSelfType { get; }

            public virtual IDummyFieldObject PrivateStaticGetSelfType { get; }

            // *

            public virtual IDummyFieldObject PublicStaticGetSetSelfType { get; set; }

            public virtual IDummyFieldObject InternalStaticGetSetSelfType { get; set; }

            public virtual IDummyFieldObject ProtectedStaticGetSetSelfType { get; set; }

            public virtual IDummyFieldObject PrivateStaticGetSetSelfType { get; set; }

            // *

            public virtual IDummyFieldObject PublicGetSelfType { get; }

            public virtual IDummyFieldObject InternalGetSelfType { get; }

            public virtual IDummyFieldObject ProtectedGetSelfType { get; }

            public virtual IDummyFieldObject PrivateGetSelfType { get; }

            // *

            public virtual IDummyFieldObject PublicGetSetSelfType { get; set; }

            public virtual IDummyFieldObject InternalGetSetSelfType { get; set; }

            public virtual IDummyFieldObject ProtectedGetSetSelfType { get; set; }

            public virtual IDummyFieldObject PrivateGetSetSelfType { get; set; }
        }
    }
}