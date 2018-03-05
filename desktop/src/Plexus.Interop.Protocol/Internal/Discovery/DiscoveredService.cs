/**
 * Copyright 2017 Plexus Interop Deutsche Bank AG
 * SPDX-License-Identifier: Apache-2.0
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
﻿namespace Plexus.Interop.Protocol.Internal.Discovery
{
    using Plexus.Interop.Protocol.Discovery;
    using Plexus.Pools;
    using System.Collections.Generic;

    internal sealed class DiscoveredService : PooledObject<DiscoveredService>, IDiscoveredService
    {
        public IConsumedServiceReference ConsumedService { get; set; }

        public IProvidedServiceReference ProvidedService { get; set; }

        public Maybe<string> ServiceTitle { get; set; }

        public IReadOnlyCollection<IDiscoveredServiceMethod> Methods { get; set; }

        protected override void Cleanup()
        {
            ConsumedService?.Dispose();
            ConsumedService = default;
            ProvidedService?.Dispose();
            ProvidedService = default;
            ServiceTitle = default;
            if (Methods != null)
            {
                foreach (var method in Methods)
                {
                    method?.Dispose();
                }
            }
            Methods = default;
        }

        public override string ToString()
        {
            return $"{nameof(ConsumedService)}: {{{ConsumedService}}}, {nameof(ProvidedService)}: {{{ProvidedService}}}, {nameof(ServiceTitle)}: {ServiceTitle}, {nameof(Methods)}: {Methods.FormatEnumerableObjects()}";
        }

        private bool Equals(DiscoveredService other)
        {
            return Equals(ConsumedService, other.ConsumedService) && Equals(ProvidedService, other.ProvidedService) && ServiceTitle.Equals(other.ServiceTitle) && Equals(Methods, other.Methods);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is DiscoveredService && Equals((DiscoveredService) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (ConsumedService != null ? ConsumedService.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (ProvidedService != null ? ProvidedService.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ ServiceTitle.GetHashCode();
                hashCode = (hashCode * 397) ^ (Methods != null ? Methods.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
