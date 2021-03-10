using LiteDB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace Acorisoft.Morisa.Core
{
    public enum BinaryObjectType
    {
        Image,
        Binary
    }
    public abstract class BinaryObject : IBinaryObject
    {
        [BsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangedEventHandler ChangedHandler;

        [BsonIgnore]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private protected PropertyChangingEventHandler ChangingHandler;
        public BinaryObject()
        {
            Id = Guid.NewGuid();
        }
        protected bool SetValueAndRaiseUpdate<T>(ref T backendField, T value, [CallerMemberName] string name = "")
        {
            if (!EqualityComparer<T>.Default.Equals(backendField, value))
            {
                ChangingHandler?.Invoke(this, new PropertyChangingEventArgs(name));
                backendField = value;
                ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
                return true;
            }

            return false;
        }

        protected void RaiseUpdate([CallerMemberName] string name = "")
        {
            ChangedHandler?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => ChangedHandler += value;
            remove => ChangedHandler -= value;
        }

        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add => ChangingHandler += value;
            remove => ChangingHandler -= value;
        }

        [BsonId]
        /// <summary>
        /// 获取或设置当前对象的唯一标识符。
        /// </summary>
        public Guid Id { get; set; }
        public string Md5 { get; set; }
        public string Name { get; set; }

        [BsonIgnore]
        public string FileName { get; set; }
        public abstract BinaryObjectType Type { get; }
    }
    public sealed class ImageObject : BinaryObject
    {
        public override sealed BinaryObjectType Type => BinaryObjectType.Image;
    }
}
