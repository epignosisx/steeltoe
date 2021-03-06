﻿// Copyright 2017 the original author or authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;

namespace Steeltoe.Management.Census.Stats.Aggregations
{
    [Obsolete("Use OpenCensus project packages")]
    public sealed class LastValue : Aggregation, ILastValue
    {
        LastValue()
        {
        }

        private static readonly LastValue INSTANCE = new LastValue();

        public static ILastValue Create()
        {
            return INSTANCE;
        }

        public override M Match<M>(Func<ISum, M> p0, Func<ICount, M> p1, Func<IMean, M> p2, Func<IDistribution, M> p3, Func<ILastValue, M> p4, Func<IAggregation, M> p5)
        {
            return p4.Invoke(this);
        }

        public override string ToString()
        {
            return "LastValue{"
                + "}";
        }

        public override bool Equals(object o)
        {
            if (o == this)
            {
                return true;
            }

            if (o is LastValue)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int h = 1;
            return h;
        }
    }
}
