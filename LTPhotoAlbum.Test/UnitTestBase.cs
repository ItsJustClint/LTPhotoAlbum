using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace LTPhotoAlbum.Test
{
    public class UnitTestBase : IDisposable
    {
        public MockRepository MockRepository { get; private set; }

        protected UnitTestBase()
        {
            MockRepository = new MockRepository(MockBehavior.Strict) { DefaultValue = DefaultValue.Empty };
        }

        public void Dispose()
        {
            MockRepository.VerifyAll();
        }
    }
}
