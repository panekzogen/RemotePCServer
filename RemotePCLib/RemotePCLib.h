// RemotePCLib.h

#pragma once

#include <WinSock2.h>
#include <ws2bth.h>
#include <bthsdpdef.h>
#include <BluetoothAPIs.h>
#include <iostream>
#include <tchar.h>

#pragma comment(lib, "Ws2_32.lib")
#pragma comment(lib, "irprops.lib")
#pragma comment(lib, "Rpcrt4.lib")

using namespace System;

namespace RemotePCLib {
	public ref class BluetoothServer		//TODO: log file
	{
		public:
			SOCKET s; 
			SOCKET s2;
			LPWSAQUERYSET servicePtr;
			System::String^ deviceName;
			System::String^ deviceAddress;
			BluetoothServer(){
				WORD wVersionRequested = 0x202;
				WSADATA m_data;
				if (0 == WSAStartup(wVersionRequested, &m_data)){
					s = socket(AF_BTH, SOCK_STREAM, BTHPROTO_RFCOMM);
					if (s == INVALID_SOCKET)
					{
						exit(1);
					}

					WSAPROTOCOL_INFO protocolInfo;
					int protocolInfoSize = sizeof(protocolInfo);

					if (0 != getsockopt(s, SOL_SOCKET, SO_PROTOCOL_INFO, (char*)&protocolInfo, &protocolInfoSize))
					{
						exit(1);
					}

					SOCKADDR_BTH address;
					address.addressFamily = AF_BTH;
					address.btAddr = 0;
					address.serviceClassId = GUID_NULL;
					address.port = BT_PORT_ANY;
					sockaddr *pAddr = (sockaddr*)&address;

					if (0 != bind(s, pAddr, sizeof(SOCKADDR_BTH)))
					{
						exit(1);
					}
					else
					{
						/*int length = sizeof(SOCKADDR_BTH);
						getsockname(s, (sockaddr*)&address, &length);
						char buffer[100];
						sprintf(buffer, "Local Bluetooth device is %04x%08x \nServer channel = %d\n",
							GET_NAP(address.btAddr), GET_SAP(address.btAddr), address.port);
						device = gcnew System::String(buffer);*/
					}

					int size = sizeof(SOCKADDR_BTH);
					if (0 != getsockname(s, pAddr, &size))
					{
						exit(1);
					}
					if (0 != listen(s, 10))
					{
						exit(1);
					}

					WSAQUERYSET service;
					memset(&service, 0, sizeof(service));
					service.dwSize = sizeof(service);
					service.lpszServiceInstanceName = _T("Accelerometer Data...");
					service.lpszComment = _T("Pushing data to PC");

					GUID serviceID;
					UuidFromStringA((unsigned char*)"b4906571-36e9-417b-9853-661efcee4196", (LPCLSID)&serviceID);

					service.lpServiceClassId = &serviceID;
					service.dwNumberOfCsAddrs = 1;
					service.dwNameSpace = NS_BTH;

					CSADDR_INFO csAddr;
					memset(&csAddr, 0, sizeof(csAddr));
					csAddr.LocalAddr.iSockaddrLength = sizeof(SOCKADDR_BTH);
					csAddr.LocalAddr.lpSockaddr = pAddr;
					csAddr.iSocketType = SOCK_STREAM;
					csAddr.iProtocol = BTHPROTO_RFCOMM;
					service.lpcsaBuffer = &csAddr;

					if (0 != WSASetService(&service, RNRSERVICE_REGISTER, 0))
					{
						exit(1);
					}

					servicePtr = &service;
				}	
			}
			~BluetoothServer(){
				closesocket(s2);
				if (0 != WSASetService(servicePtr, RNRSERVICE_DELETE, 0))
				{
					exit(1);
				}
				closesocket(s);
				WSACleanup();
			}
			int clientConnect(){
				SOCKADDR_BTH sab2;
				int ilen = sizeof(sab2);
				s2 = accept(s, (sockaddr*)&sab2, &ilen);
				if (s2 == INVALID_SOCKET)
				{
					return 0;
				}
				char buffer[100];
				sprintf(buffer, "%04x%08x::%d",	GET_NAP(sab2.btAddr), GET_SAP(sab2.btAddr), sab2.port);
				deviceAddress = gcnew System::String(buffer);
				servicePtr->lpszServiceInstanceName;
				sprintf(buffer, "%04x%08x::%d", GET_NAP(sab2.btAddr), GET_SAP(sab2.btAddr), sab2.port);				
				return 1;
			}
			System::String^ getCommand(void){
				char buffer[1024] = { 0 };
				memset(buffer, 0, sizeof(buffer));
				int r = recv(s2, (char*)buffer, sizeof(buffer), 0);
				if (r == 0) return nullptr;
				System::String^ s = gcnew System::String(buffer);
				return s;
			}
	};
}
