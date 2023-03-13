#pragma warning disable IDE0073

// Copyright (c) 2011 Google, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
// CityHash, by Geoff Pike and Jyrki Alakuijala
//
// This file provides CityHash64() and related functions.
//
// It's probably possible to create even faster hash functions by
// writing a program that systematically explores some of the space of
// possible hash functions, by using SIMD instructions, or by
// compromising on hash quality.

using System;

namespace UAssetAPI
{
#pragma warning disable IDE0054
#pragma warning disable IDE0180
#pragma warning disable IDE1006
#pragma warning disable CS1591 // Missing documentation

    public class CityHash
    {
        // Some primes between 2^63 and 2^64 for various uses.
        const ulong k0 = 0xc3a5c85c97cb3127;
        const ulong k1 = 0xb492b66fbe98f273;
        const ulong k2 = 0x9ae16a3b2f90404f;

        // Magic numbers for 32-bit hashing.  Copied from Murmur3.
        const uint c1 = 0xcc9e2d51;
        const uint c2 = 0x1b873593;

        public struct Uint128_64
        {
            public ulong lo;
            public ulong hi;
        }

        private static uint bswap_32(uint x)
        {
            return (x >> 24) + ((x >> 8) & 0xff00) + ((x << 8) & 0xff0000) + (x << 24);
        }

        private static ulong bswap_64(ulong x)
        {
            return (x >> 56) + ((x >> 40) & 0xff00) + ((x >> 24) & 0xff0000) + ((x >> 8) & 0xff000000) + ((x << 8) & 0xff00000000L) + ((x << 24) & 0xff0000000000L) + ((x << 40) & 0xff000000000000L) + (x << 56);
        }

        private static unsafe uint Fetch32(byte* data)
        {
#if BIGENDIAN
			return (uint)data[3] + ((uint)data[2] << 8) + ((uint)data[1] << 16) + ((uint)data[0] << 24);
#else
            return (uint)data[0] + ((uint)data[1] << 8) + ((uint)data[2] << 16) + ((uint)data[3] << 24);
#endif
        }

        private static unsafe ulong Fetch64(byte* data)
        {
#if BIGENDIAN
			return ((ulong)Fetch32(data) << 32) + (ulong)Fetch32(data + 4);
#else
            return (ulong)Fetch32(data) + ((ulong)Fetch32(data + 4) << 32);
#endif
        }

        private static uint fmix(uint h)
        {
            h ^= h >> 16;
            h *= 0x85ebca6b;
            h ^= h >> 13;
            h *= 0xc2b2ae35;
            h ^= h >> 16;
            return h;
        }

        private static uint Rotate32(uint val, int shift)
        {
            // Avoid shifting by 32: doing so yields an undefined result.
            return shift == 0 ? val : ((val >> shift) | (val << (32 - shift)));
        }

        private static void SwapValues<T>(ref T a, ref T b)
        {
            T c = a;
            a = b;
            b = c;
        }

        private static void PERMUTE3<T>(ref T a, ref T b, ref T c)
        {
            //SwapValues(ref a, ref b);
            //SwapValues(ref a, ref c);
            T d = c;
            c = b;
            b = a;
            a = d;
        }

        private static uint Mur(uint a, uint h)
        {
            // Helper from Murmur3 for combining two 32-bit values.
            a *= c1;
            a = Rotate32(a, 17);
            a *= c2;
            h ^= a;
            h = Rotate32(h, 19);
            return h * 5 + 0xe6546b64;
        }

        private static unsafe uint Hash32Len13to24(byte* s, uint len)
        {
            uint a = Fetch32(s - 4 + (len >> 1));
            uint b = Fetch32(s + 4);
            uint c = Fetch32(s + len - 8);
            uint d = Fetch32(s + (len >> 1));
            uint e = Fetch32(s);
            uint f = Fetch32(s + len - 4);
            uint h = len;

            return fmix(Mur(f, Mur(e, Mur(d, Mur(c, Mur(b, Mur(a, h)))))));
        }

        private static unsafe uint Hash32Len0to4(byte* s, uint len)
        {
            uint b = 0;
            uint c = 9;
            for (uint i = 0; i < len; i++)
            {
                char v = (char)s[i];
                b = b * c1 + v;
                c ^= b;
            }
            return fmix(Mur(b, Mur(len, c)));
        }

        private static unsafe uint Hash32Len5to12(byte* s, uint len)
        {
            uint a = len, b = len * 5, c = 9, d = b;
            a += Fetch32(s);
            b += Fetch32(s + len - 4);
            c += Fetch32(s + ((len >> 1) & 4));
            return fmix(Mur(c, Mur(b, Mur(a, d))));
        }

        public static unsafe uint CityHash32(byte* s, uint len)
        {

            if (len <= 24)
            {
                return len <= 12 ?
                    (len <= 4 ? Hash32Len0to4(s, len) : Hash32Len5to12(s, len)) :
                    Hash32Len13to24(s, len);
            }

            // len > 24
            uint h = len, g = c1 * len, f = g;
            uint a0 = Rotate32(Fetch32(s + len - 4) * c1, 17) * c2;
            uint a1 = Rotate32(Fetch32(s + len - 8) * c1, 17) * c2;
            uint a2 = Rotate32(Fetch32(s + len - 16) * c1, 17) * c2;
            uint a3 = Rotate32(Fetch32(s + len - 12) * c1, 17) * c2;
            uint a4 = Rotate32(Fetch32(s + len - 20) * c1, 17) * c2;
            h ^= a0;
            h = Rotate32(h, 19);
            h = h * 5 + 0xe6546b64;
            h ^= a2;
            h = Rotate32(h, 19);
            h = h * 5 + 0xe6546b64;
            g ^= a1;
            g = Rotate32(g, 19);
            g = g * 5 + 0xe6546b64;
            g ^= a3;
            g = Rotate32(g, 19);
            g = g * 5 + 0xe6546b64;
            f += a4;
            f = Rotate32(f, 19);
            f = f * 5 + 0xe6546b64;
            uint iters = (len - 1) / 20;
            do
            {
                uint _a0 = Rotate32(Fetch32(s) * c1, 17) * c2;
                uint _a1 = Fetch32(s + 4);
                uint _a2 = Rotate32(Fetch32(s + 8) * c1, 17) * c2;
                uint _a3 = Rotate32(Fetch32(s + 12) * c1, 17) * c2;
                uint _a4 = Fetch32(s + 16);
                h ^= _a0;
                h = Rotate32(h, 18);
                h = h * 5 + 0xe6546b64;
                f += _a1;
                f = Rotate32(f, 19);
                f = f * c1;
                g += _a2;
                g = Rotate32(g, 18);
                g = g * 5 + 0xe6546b64;
                h ^= _a3 + _a1;
                h = Rotate32(h, 19);
                h = h * 5 + 0xe6546b64;
                g ^= _a4;
                g = bswap_32(g) * 5;
                h += _a4 * 5;
                h = bswap_32(h);
                f += _a0;
                PERMUTE3(ref f, ref h, ref g);
                s += 20;
            } while (--iters != 0);
            g = Rotate32(g, 11) * c1;
            g = Rotate32(g, 17) * c1;
            f = Rotate32(f, 11) * c1;
            f = Rotate32(f, 17) * c1;
            h = Rotate32(h + g, 19);
            h = h * 5 + 0xe6546b64;
            h = Rotate32(h, 17) * c1;
            h = Rotate32(h + f, 19);
            h = h * 5 + 0xe6546b64;
            h = Rotate32(h, 17) * c1;
            return h;
        }

        // Bitwise right rotate.  Normally this will compile to a single
        // instruction, especially if the shift is a manifest constant.
        private static ulong Rotate(ulong val, int shift)
        {
            // Avoid shifting by 64: doing so yields an undefined result.
            return shift == 0 ? val : ((val >> shift) | (val << (64 - shift)));
        }

        private static ulong ShiftMix(ulong val)
        {
            return val ^ (val >> 47);
        }

        private static ulong HashLen16(ulong u, ulong v)
        {
            return CityHash128to64(new Uint128_64 { lo = u, hi = v });
        }

        private static ulong HashLen16(ulong u, ulong v, ulong mul)
        {
            // Murmur-inspired hashing.
            ulong a = (u ^ v) * mul;
            a ^= (a >> 47);
            ulong b = (v ^ a) * mul;
            b ^= (b >> 47);
            b *= mul;
            return b;
        }

        private static unsafe ulong HashLen0to16(byte* s, uint len)
        {
            if (len >= 8)
            {
                ulong mul = k2 + len * 2;
                ulong a = Fetch64(s) + k2;
                ulong b = Fetch64(s + len - 8);
                ulong c = Rotate(b, 37) * mul + a;
                ulong d = (Rotate(a, 25) + b) * mul;
                return HashLen16(c, d, mul);
            }
            if (len >= 4)
            {
                ulong mul = k2 + len * 2;
                ulong a = Fetch32(s);
                return HashLen16(len + (a << 3), Fetch32(s + len - 4), mul);
            }
            if (len > 0)
            {
                byte a = s[0];
                byte b = s[len >> 1];
                byte c = s[len - 1];
                uint y = (uint)a + ((uint)b << 8);
                uint z = len + ((uint)c << 2);
                return ShiftMix(y * k2 ^ z * k0) * k2;
            }
            return k2;
        }

        // This probably works well for 16-byte strings as well, but it may be overkill
        // in that case.
        private static unsafe ulong HashLen17to32(byte* s, uint len)
        {
            ulong mul = k2 + len * 2;
            ulong a = Fetch64(s) * k1;
            ulong b = Fetch64(s + 8);
            ulong c = Fetch64(s + len - 8) * mul;
            ulong d = Fetch64(s + len - 16) * k2;
            return HashLen16(Rotate(a + b, 43) + Rotate(c, 30) + d,
                a + Rotate(b + k2, 18) + c, mul);
        }

        // Return a 16-byte hash for 48 bytes.  Quick and dirty.
        // Callers do best to use "random-looking" values for a and b.
        private static unsafe Uint128_64 WeakHashLen32WithSeeds(
            ulong w, ulong x, ulong y, ulong z, ulong a, ulong b)
        {
            a += w;
            b = Rotate(b + a + z, 21);
            ulong c = a;
            a += x;
            a += y;
            b += Rotate(a, 44);
            return new Uint128_64 { lo = (a + z), hi = (b + c) };
        }

        // Return a 16-byte hash for s[0] ... s[31], a, and b.  Quick and dirty.
        private static unsafe Uint128_64 WeakHashLen32WithSeeds(byte* s, ulong a, ulong b)
        {
            return WeakHashLen32WithSeeds(Fetch64(s),
                Fetch64(s + 8),
                Fetch64(s + 16),
                Fetch64(s + 24),
                a,
                b);
        }

        // Return an 8-byte hash for 33 to 64 bytes.
        private static unsafe ulong HashLen33to64(byte* s, uint len)
        {
            ulong mul = k2 + len * 2;
            ulong a = Fetch64(s) * k2;
            ulong b = Fetch64(s + 8);
            ulong c = Fetch64(s + len - 24);
            ulong d = Fetch64(s + len - 32);
            ulong e = Fetch64(s + 16) * k2;
            ulong f = Fetch64(s + 24) * 9;
            ulong g = Fetch64(s + len - 8);
            ulong h = Fetch64(s + len - 16) * mul;
            ulong u = Rotate(a + g, 43) + (Rotate(b, 30) + c) * 9;
            ulong v = ((a + g) ^ d) + f + 1;
            ulong w = bswap_64((u + v) * mul) + h;
            ulong x = Rotate(e + f, 42) + c;
            ulong y = (bswap_64((v + w) * mul) + g) * mul;
            ulong z = e + f + c;
            a = bswap_64((x + z) * mul + y) + b;
            b = ShiftMix((z + a) * mul + d + h) * mul;
            return b + x;
        }

        public static unsafe ulong CityHash64(byte* s, uint len)
        {
            if (len <= 32)
            {
                if (len <= 16)
                {
                    return HashLen0to16(s, len);
                }
                else
                {
                    return HashLen17to32(s, len);
                }
            }
            else if (len <= 64)
            {
                return HashLen33to64(s, len);
            }

            // For strings over 64 bytes we hash the end first, and then as we
            // loop we keep 56 bytes of state: v, w, x, y, and z.
            ulong x = Fetch64(s + len - 40);
            ulong y = Fetch64(s + len - 16) + Fetch64(s + len - 56);
            ulong z = HashLen16(Fetch64(s + len - 48) + len, Fetch64(s + len - 24));
            Uint128_64 v = WeakHashLen32WithSeeds(s + len - 64, len, z);
            Uint128_64 w = WeakHashLen32WithSeeds(s + len - 32, y + k1, x);
            x = x * k1 + Fetch64(s);

            // Decrease len to the nearest multiple of 64, and operate on 64-byte chunks.
            len = (len - 1) & ~(uint)63;
            do
            {
                x = Rotate(x + y + v.lo + Fetch64(s + 8), 37) * k1;
                y = Rotate(y + v.hi + Fetch64(s + 48), 42) * k1;
                x ^= w.hi;
                y += v.lo + Fetch64(s + 40);
                z = Rotate(z + w.lo, 33) * k1;
                v = WeakHashLen32WithSeeds(s, v.hi * k1, x + w.lo);
                w = WeakHashLen32WithSeeds(s + 32, z + w.hi, y + Fetch64(s + 16));
                SwapValues(ref z, ref x);
                s += 64;
                len -= 64;
            } while (len != 0);
            return HashLen16(HashLen16(v.lo, w.lo) + ShiftMix(y) * k1 + z,
                HashLen16(v.hi, w.hi) + x);
        }

        public static unsafe ulong CityHash64WithSeed(byte* s, uint len, ulong seed)
        {
            return CityHash64WithSeeds(s, len, k2, seed);
        }

        public static unsafe ulong CityHash64WithSeeds(byte* s, uint len, ulong seed0, ulong seed1)
        {
            return HashLen16(CityHash64(s, len) - seed0, seed1);
        }

        // Hash 128 input bits down to 64 bits of output.
        // This is intended to be a reasonably good hash function.
        public static ulong CityHash128to64(Uint128_64 x)
        {
            // Murmur-inspired hashing.
            const ulong kMul = 0x9ddfea08eb382d69L;
            ulong a = (x.lo ^ x.hi) * kMul;
            a ^= (a >> 47);
            ulong b = (x.hi ^ a) * kMul;
            b ^= (b >> 47);
            b *= kMul;
            return b;
        }
    }
}
#pragma warning restore IDE1006
#pragma warning restore IDE0180
#pragma warning restore IDE0054