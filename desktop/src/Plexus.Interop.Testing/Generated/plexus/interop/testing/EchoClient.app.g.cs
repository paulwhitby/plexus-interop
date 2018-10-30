/**
 * Copyright 2017-2018 Plexus Interop Deutsche Bank AG
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
// <auto-generated>
// 	Generated by the Plexus Interop compiler.  DO NOT EDIT!
// 	source: plexus\interop\testing\echo_client.interop
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code
namespace Plexus.Interop.Testing.Generated {
	
	using System;
	using global::Plexus;
	using global::Plexus.Channels;
	using global::Plexus.Interop;
	using global::System.Threading.Tasks;
					
					
	public partial interface IEchoClient: IClient {
		EchoClient.IEchoServiceProxy EchoService { get; }
		
		EchoClient.IGreetingServiceProxy GreetingService { get; }
	}
	
	public sealed partial class EchoClient: ClientBase, IEchoClient {
		
		public const string Id = "plexus.interop.testing.EchoClient";
		
		private static ClientOptions CreateClientOptions(Func<ClientOptionsBuilder, ClientOptionsBuilder> setup = null) {
			ClientOptionsBuilder builder = new ClientOptionsBuilder().WithApplicationId(Id).WithDefaultConfiguration();
			if (setup != null) {
				builder = setup(builder);
			}									
			return builder.Build();					
		}
		
		public EchoClient(Func<ClientOptionsBuilder, ClientOptionsBuilder> setup = null): base(CreateClientOptions(setup)) 
		{ 
			EchoService = new EchoClient.EchoServiceProxy(this.CallInvoker);
			GreetingService = new EchoClient.GreetingServiceProxy(this.CallInvoker);
		}
		
		public partial interface IEchoServiceProxy:
			global::Plexus.Interop.Testing.Generated.EchoService.IUnaryProxy,
			global::Plexus.Interop.Testing.Generated.EchoService.IServerStreamingProxy,
			global::Plexus.Interop.Testing.Generated.EchoService.IClientStreamingProxy,
			global::Plexus.Interop.Testing.Generated.EchoService.IDuplexStreamingProxy
		{ }
		
		public sealed partial class EchoServiceProxy: IEchoServiceProxy {
			
			public static global::Plexus.Interop.Testing.Generated.EchoService.Descriptor Descriptor = global::Plexus.Interop.Testing.Generated.EchoService.DefaultDescriptor;
			
			private readonly IClientCallInvoker _callInvoker;
									
			public EchoServiceProxy(IClientCallInvoker callInvoker) {
				_callInvoker = callInvoker;
			}						
			
			public IUnaryMethodCall<global::Plexus.Interop.Testing.Generated.EchoRequest> Unary(global::Plexus.Interop.Testing.Generated.EchoRequest request) {
				return _callInvoker.Call(Descriptor.UnaryMethod, request);
			}
			
			public IServerStreamingMethodCall<global::Plexus.Interop.Testing.Generated.EchoRequest> ServerStreaming(global::Plexus.Interop.Testing.Generated.EchoRequest request) {
				return _callInvoker.Call(Descriptor.ServerStreamingMethod, request);
			}
			
			public IClientStreamingMethodCall<global::Plexus.Interop.Testing.Generated.EchoRequest, global::Plexus.Interop.Testing.Generated.EchoRequest> ClientStreaming() {
				return _callInvoker.Call(Descriptor.ClientStreamingMethod);
			}
			
			public IDuplexStreamingMethodCall<global::Plexus.Interop.Testing.Generated.EchoRequest, global::Plexus.Interop.Testing.Generated.EchoRequest> DuplexStreaming() {
				return _callInvoker.Call(Descriptor.DuplexStreamingMethod);
			}
		}
		
		public IEchoServiceProxy EchoService { get; private set; }
		
		public partial interface IGreetingServiceProxy:
			global::Plexus.Interop.Testing.Generated.GreetingService.IHelloProxy
		{ }
		
		public sealed partial class GreetingServiceProxy: IGreetingServiceProxy {
			
			public static global::Plexus.Interop.Testing.Generated.GreetingService.Descriptor Descriptor = global::Plexus.Interop.Testing.Generated.GreetingService.DefaultDescriptor;
			
			private readonly IClientCallInvoker _callInvoker;
									
			public GreetingServiceProxy(IClientCallInvoker callInvoker) {
				_callInvoker = callInvoker;
			}						
			
			public IUnaryMethodCall<global::Plexus.Interop.Testing.Generated.GreetingResponse> Hello(global::Plexus.Interop.Testing.Generated.GreetingRequest request) {
				return _callInvoker.Call(Descriptor.HelloMethod, request);
			}
		}
		
		public IGreetingServiceProxy GreetingService { get; private set; }
	}
}
#endregion Designer generated code