using System;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using LanguageExt;
using Rewind.Extensions;
using UnityEngine;

namespace Code.Helpers.FileSystem
{
    [Serializable, PublicAPI]
    public partial struct PathStr : IComparable<PathStr>
    {
        #region Unity Serialized Fields

#pragma warning disable 649
        // ReSharper disable NotNullMemberIsNotInitialized, FieldCanBeMadeReadOnly.Local, ConvertToConstant.Local
        [SerializeField] private string path;
        // ReSharper restore NotNullMemberIsNotInitialized, FieldCanBeMadeReadOnly.Local, ConvertToConstant.Local
#pragma warning restore 649

        #endregion

        public string Path => path;

        public PathStr(string path)
        {
            this.path = path.Replace(System.IO.Path.DirectorySeparatorChar == '/' ? '\\' : '/', System.IO.Path.DirectorySeparatorChar);
        }
        
        public static PathStr A(string path) => new PathStr(path);

        #region Comparable

        public int CompareTo(PathStr other) => string.Compare(Path, other.Path, StringComparison.Ordinal);

        private sealed class PathRelationalComparer : Comparer<PathStr>
        {
            public override int Compare(PathStr x, PathStr y)
            {
                return string.Compare(x.Path, y.Path, StringComparison.Ordinal);
            }
        }

        public static Comparer<PathStr> PathComparer { get; } = new PathRelationalComparer();

        #endregion

        public static PathStr operator /(PathStr s1, string s2) => new PathStr(System.IO.Path.Combine(s1.Path, s2));
        public static implicit operator string(PathStr s) => s.Path;

        public PathStr Dirname => new PathStr(System.IO.Path.GetDirectoryName(Path));
        public PathStr Basename => new PathStr(System.IO.Path.GetFileName(Path));
        public string Extension => System.IO.Path.GetExtension(Path);
        
        /// <summary>Removes a single file extension from the path.</summary>
        public PathStr WithoutExtension => A(path.Substring(0, path.Length - Extension.Length));

        /// <summary>Removes all file extensions from the path.</summary>
        public PathStr WithoutExtensions
        {
            get
            {
                // TODO: optimize
                var current = this;
                while (true)
                {
                    var updated = current.WithoutExtension;
                    
                    if (current == updated) return current;
                    else current = updated;
                }
            }
        }

        public PathStr EnsureBeginsWith(PathStr p) => Path.StartsWithFast(p.Path) ? this : p / Path;
        public override string ToString() => AsString();
        public readonly string AsString() => path;
        
        /// <summary>Path in UNIX format (with / slashes).</summary>
        public string UnixString => ToString().Replace('\\', '/');

        /// <summary>
        /// Use this with Unity Resources, AssetDatabase and PrefabUtility methods
        /// </summary>
        public string UnityPath => System.IO.Path.DirectorySeparatorChar == '/' ? Path : Path.Replace('\\' , '/');
        
        public PathStr ToAbsolute => A(System.IO.Path.GetFullPath(Path));
        
        public bool StartsWith(PathStr path, bool ignoreCase=false) => this.path.StartsWithFast(path.path, ignoreCase);
        public bool EndsWith(PathStr path, bool ignoreCase=false) => this.path.EndsWithFast(path.path, ignoreCase);
    }

    public static class PathStrExts
    {
        private static Option<PathStr> OnCondition(this string s, bool condition) =>
            (condition && s != null).Opt(new PathStr(s));

        public static Option<PathStr> AsFile(this string s) => s.OnCondition(File.Exists(s));
        public static Option<PathStr> AsDirectory(this string s) => s.OnCondition(Directory.Exists(s));
    }
}