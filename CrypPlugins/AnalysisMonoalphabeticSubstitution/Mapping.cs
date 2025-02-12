﻿using System;

namespace CrypTool.AnalysisMonoalphabeticSubstitution
{
    internal class Mapping
    {
        #region Variables

        private int size;
        private readonly bool[][] map;

        #endregion

        #region Constructor

        public Mapping(int size)
        {
            this.size = size;
            map = new bool[size][];
            for (int i = 0; i < map.Length; i++)
            {
                map[i] = new bool[size];
            }
        }

        #endregion

        #region Methods

        public int[] DeriveKey()
        {
            int[] m = new int[map.Length];

            for (int i = 0; i < map.Length; i++)
            {
                m[i] = GetMapping((byte)i);
            }

            return m;
        }

        public Mapping Copy()
        {
            Mapping ms = new Mapping(size);
            ms.SetTo(this);

            return ms;
        }

        public bool HasMapping(int c)
        {
            for (int i = 0; i < map[c].Length; i++)
            {
                if (map[c][i])
                {
                    return true;
                }
            }
            return false;
        }

        public void SetTo(Mapping mapping)
        {
            size = mapping.size;
            for (int i = 0; i < mapping.map.Length; i++)
            {
                Array.Copy(mapping.map[i], map[i], mapping.map[i].Length);
            }
        }

        public void SetEmpty()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j] = false;
                }
            }
        }

        public void SetFull()
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    map[i][j] = true;
                }
            }
        }

        public bool IsUniquelyMapped(byte a)
        {
            int unique = 0;

            for (int i = 0; i < map[a].Length; i++)
            {
                if (map[a][i] == true)
                {
                    unique++;
                    if (unique > 1)
                    {
                        return false;
                    }
                }
            }
            if (unique == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool IsMappingOK(byte[] a, byte[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (map[a[i]][b[i]] == false)
                {
                    return false;
                }
            }
            return true;
        }

        public void SetMapping(byte[] a, byte[] b)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < b.Length; j++)
                {
                    map[i][b[j]] = false;
                }
            }
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < map[a[i]].Length; j++)
                {
                    map[a[i]][j] = false;
                }
                map[a[i]][b[i]] = true;
            }
        }

        public void EnableMapping(byte[] a, byte[] b)
        {
            for (int i = 0; i < a.Length; i++)
            {
                map[a[i]][b[i]] = true;
            }
        }

        public void IntersectWith(Mapping mapping)
        {
            bool[] letintersect;
            for (int i = 0; i < map.Length; i++)
            {
                int unique = 0;
                int uniqueletter = 0;
                for (int j = 0; j < mapping.map[i].Length; j++)
                {
                    if (mapping.map[i][j] == true)
                    {
                        unique++;
                        uniqueletter = j;
                    }
                }

                if (unique > 0)
                {
                    letintersect = new bool[map[i].Length];

                    for (int j = 0; j < map[i].Length; j++)
                    {
                        if (map[i][j] && mapping.map[i][j])
                        {
                            letintersect[j] = true;
                        }
                    }
                    map[i] = letintersect;
                }

                if (unique == 1)
                {
                    for (int x = 0; x < map.Length; x++)
                    {
                        if (x != i)
                        {
                            map[x][uniqueletter] = false;
                        }
                    }
                }
            }

            //  bool res1 = Compare();
            //  Debug.Assert(res1);
        }

        public int GetMapping(byte a)
        {
            int unique = 0;
            int uniqueletter = 0;

            for (int i = 0; i < map[a].Length; i++)
            {
                if (map[a][i] == true)
                {
                    unique++;
                    uniqueletter = i;
                }
            }

            if (unique == 1)
            {
                return uniqueletter;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }
}


