// SPDX-FileCopyrightText: 2024 Unity Technologies and the glTFast authors
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;
using GLTFast.Schema;
using UnityEngine;

namespace GLTFast
{
    /// <summary>
    /// Compares Mesh Primitives based on their attributes and morph targets only.
    /// This is practical when clustering primitives of a mesh together,
    /// that end up in a single Unity Mesh.
    /// </summary>
    class MeshPrimitiveComparer : IEqualityComparer<MeshPrimitiveBase>
    {
        public bool Equals(MeshPrimitiveBase x, MeshPrimitiveBase y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;

            return Equals(x.attributes, y.attributes)
                && Equals(x.targets, y.targets);
        }

        public int GetHashCode(MeshPrimitiveBase obj)
        {
#if NET_STANDARD
            return HashCode.Combine(
                GetHashCode(obj.attributes),
                GetHashCode(obj.targets)
                );
#else
            var hash = 13;
            hash = hash * 31 + GetHashCode(obj.attributes);
            hash = hash * 31 + GetHashCode(obj.targets);
            return hash;
#endif
        }

        static int GetHashCode(Attributes x)
        {
            if (x == null) return 0;
#if NET_STANDARD
            HashCode hash = new();
            hash.Add(x.POSITION);
            hash.Add(x.NORMAL);
            hash.Add(x.TANGENT);
            hash.Add(x.TEXCOORD_0);
            hash.Add(x.TEXCOORD_1);
            hash.Add(x.TEXCOORD_2);
            hash.Add(x.TEXCOORD_3);
            hash.Add(x.TEXCOORD_4);
            hash.Add(x.TEXCOORD_5);
            hash.Add(x.TEXCOORD_6);
            hash.Add(x.TEXCOORD_7);
            hash.Add(x.COLOR_0);
            hash.Add(x.JOINTS_0);
            hash.Add(x.WEIGHTS_0);
            return hash.ToHashCode();
#else
            var hash = 17;
            hash = hash * 31 + x.POSITION;
            hash = hash * 31 + x.NORMAL;
            hash = hash * 31 + x.TANGENT;
            hash = hash * 31 + x.TEXCOORD_0;
            hash = hash * 31 + x.TEXCOORD_1;
            hash = hash * 31 + x.TEXCOORD_2;
            hash = hash * 31 + x.TEXCOORD_3;
            hash = hash * 31 + x.TEXCOORD_4;
            hash = hash * 31 + x.TEXCOORD_5;
            hash = hash * 31 + x.TEXCOORD_6;
            hash = hash * 31 + x.TEXCOORD_7;
            hash = hash * 31 + x.COLOR_0;
            hash = hash * 31 + x.JOINTS_0;
            hash = hash * 31 + x.WEIGHTS_0;
            return hash;
#endif
        }

        static int GetHashCode(MorphTarget[] x)
        {
            if (x == null) return 0;
#if NET_STANDARD
            HashCode hash = new();
            hash.Add(x.Length);
            foreach (var target in x)
            {
                if (target == null)
                {
                    hash.Add(0);
                    continue;
                }
                hash.Add(target.POSITION);
                hash.Add(target.NORMAL);
                hash.Add(target.TANGENT);
            }
            return hash.ToHashCode();
#else
            var hash = 17;
            hash = hash * 31 + x.Length;
            foreach (var target in x)
            {
                if (target == null)
                    continue;
                hash = hash * 31 + target.POSITION;
                hash = hash * 31 + target.NORMAL;
                hash = hash * 31 + target.TANGENT;
            }
            return hash;
#endif
        }

        static bool Equals(MorphTarget[] x, MorphTarget[] y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            if (x.Length != y.Length) return false;
            for (var i = 0; i < x.Length; i++)
            {
                if (!Equals(x[i], y[i]))
                    return false;
            }
            return true;
        }

        static bool Equals(MorphTarget x, MorphTarget y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.POSITION == y.POSITION
                && x.NORMAL == y.NORMAL
                && x.TANGENT == y.TANGENT;
        }

        static bool Equals(Attributes x, Attributes y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x == null || y == null) return false;
            return x.POSITION == y.POSITION
                && x.NORMAL == y.NORMAL
                && x.TANGENT == y.TANGENT
                && x.TEXCOORD_0 == y.TEXCOORD_0
                && x.TEXCOORD_1 == y.TEXCOORD_1
                && x.TEXCOORD_2 == y.TEXCOORD_2
                && x.TEXCOORD_3 == y.TEXCOORD_3
                && x.TEXCOORD_4 == y.TEXCOORD_4
                && x.TEXCOORD_5 == y.TEXCOORD_5
                && x.TEXCOORD_6 == y.TEXCOORD_6
                && x.TEXCOORD_7 == y.TEXCOORD_7
                && x.COLOR_0 == y.COLOR_0
                && x.JOINTS_0 == y.JOINTS_0
                && x.WEIGHTS_0 == y.WEIGHTS_0;
        }
    }
}