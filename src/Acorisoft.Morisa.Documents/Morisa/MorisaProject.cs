using Acorisoft.Morisa.Core;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Acorisoft.Morisa
{
    public class MorisaProject : IMorisaProject
    {
        public const string MorisaProjectSuffix = ".Morisa-Proj";
        public const string MorisaProjectMainDBName = "Main";
        public const string MorisaProjectMainDBFullName = MorisaProjectMainDBName + MorisaProjectSuffix;
        public const int MorisaProjectMainDBSize = 4 * 1024 * 1024;

        protected class Profile
        {
            public string Name { get; set; }
            public string Topic { get; set; }
            public string Summary { get; set; }
            public BinaryObject Cover { get; set; }
        }


        internal MorisaProject(string directory) : this(directory, Path.Combine(directory, MorisaProjectMainDBFullName))
        {

        }

        internal MorisaProject(string directory, string fileName)
        {
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
            DefinitionSetDatabase = new LiteDatabase(new ConnectionString
            {
                Filename = fileName,
                Connection = ConnectionType.Direct,
                InitialSize = MorisaProjectMainDBSize
            });
        }

        protected internal ILiteDatabase DefinitionSetDatabase { get; }

        public string Directory { get; }

        public string Name {
            get;
            set;
        }

        public string Summary {
            get;
            set;
        }

        public string Cover {
            get;
            set;
        }

        public string Topic {
            get;
            set;
        }
    }
}
