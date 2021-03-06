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
using System.Collections.Generic;

namespace Steeltoe.Management.Census.Stats
{
    [Obsolete("Use OpenCensus project packages")]
    public sealed class ViewManager : ViewManagerBase
    {
        private readonly StatsManager statsManager;

        internal ViewManager(StatsManager statsManager)
        {
            this.statsManager = statsManager;
        }

        public override void RegisterView(IView view)
        {
            statsManager.RegisterView(view);
        }

        public override IViewData GetView(IViewName viewName)
        {
            return statsManager.GetView(viewName);
        }

        public override ISet<IView> AllExportedViews
        {
            get
            {
                return statsManager.ExportedViews;
            }
        }

        internal void ClearStats()
        {
            statsManager.ClearStats();
        }

        internal void ResumeStatsCollection()
        {
            statsManager.ResumeStatsCollection();
        }
    }
}
