﻿using System;
using System.Security.Cryptography;

namespace E2
{
    public class Q2BitCoin
    {
        private SHA256Managed Hasher= new SHA256Managed();

        /// <summary>
        /// این پیاده سازی کار نمیکنه چون فقط یک عدد را امتحان میکند
        /// باید این پیاده سازی را طوری عوض کنید که یک 
        /// nonce 
        /// درست را پیدا کند.
        /// </summary>
        public bool Mine(byte[] data, int difficultyLevel, out uint nonce)
        {
            Random rnd = new Random(0);

            // Try one random value

            byte[] doubleHash;
            int zeroBytes = 0;
            nonce = 20000000;
            while (zeroBytes < difficultyLevel)
            {
                // Copy nonce to the end of data
                BitConverter.GetBytes(nonce).CopyTo(data, sizeof(uint));

                // Calculate Hash

                doubleHash = Hasher.ComputeHash(Hasher.ComputeHash(data));

                // How many zero bytes does it have at the end?
                zeroBytes = CountEndingZeroBytes(
                    doubleHash,
                    difficultyLevel);
                nonce++;
                if (nonce > uint.MaxValue)
                    break;
            }

            

            // Return if the number of zero bytes is enough
            return zeroBytes >= difficultyLevel;
        }

        public static int CountEndingZeroBytes(byte[] doubleHash, int? maxBytesToCheck = null)
        {
            int zeroBytes = 0;
            for (int i = doubleHash.Length - 1;
                     i >= doubleHash.Length - (maxBytesToCheck??doubleHash.Length);
                     i--, zeroBytes++)
            {
                if (doubleHash[i] > 0)
                    break;
            }
            return zeroBytes;
        }
    }
}