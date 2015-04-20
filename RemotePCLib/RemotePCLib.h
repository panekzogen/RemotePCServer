// RemotePCLib.h

#pragma once

#include <WinSock2.h>
#include <ws2bth.h>
#include <bthsdpdef.h>
#include <BluetoothAPIs.h>
#include <iostream>
#include <tchar.h>
#include <vcclr.h>

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
			System::String^ deviceAddress;
			System::String^ deviceName;
			System::String^ localDevice;
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
						int length = sizeof(SOCKADDR_BTH);
						getsockname(s, (sockaddr*)&address, &length);
						char buffer[100];
						sprintf(buffer, "%04x%08x",
							GET_NAP(address.btAddr), GET_SAP(address.btAddr), address.port);
						localDevice = gcnew System::String(buffer);
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
			void forceStop(){
				closesocket(s2);
				if (0 == WSASetService(servicePtr, RNRSERVICE_DELETE, 0))
				{
					exit(1);
				}
				closesocket(s);
				WSACleanup();
			}
			bool clientConnect(){
				SOCKADDR_BTH sab2;
				int ilen = sizeof(sab2);
				s2 = accept(s, (sockaddr*)&sab2, &ilen);
				if (s2 == INVALID_SOCKET)
				{
					return false;
				}
				char buffer[100];
				sprintf(buffer, "%04x%08x", GET_NAP(sab2.btAddr), GET_SAP(sab2.btAddr), sab2.port);
				deviceAddress = gcnew System::String(buffer);
				if (deviceAddress == localDevice) return false;
				deviceName = getCommand();
				return true;
			}
			System::String^ getCommand(void){
				char buffer[1024] = { 0 };
				memset(buffer, 0, sizeof(buffer));
				int r = recv(s2, (char*)buffer, sizeof(buffer), 0);
				if (r == 0) return nullptr;
				System::String^ s = gcnew System::String(buffer);
				return s;
			}
			void disconnect(){
				closesocket(s2);
			}
			void refreshInfo(){
				HBLUETOOTH_DEVICE_FIND m_bt_dev = NULL;
				/*BLUETOOTH_DEVICE_INFO m_device_info = { sizeof(BLUETOOTH_DEVICE_INFO), 0, };
				BLUETOOTH_FIND_RADIO_PARAMS m_bt_find_radio = { sizeof(BLUETOOTH_FIND_RADIO_PARAMS) };
				BLUETOOTH_DEVICE_SEARCH_PARAMS m_search_params = {
					sizeof(BLUETOOTH_DEVICE_SEARCH_PARAMS),
					1,
					0,
					1,
					1,
					1,
					15,
					NULL
				};
				char buffer[100];
				HANDLE m_radio = NULL;
				BluetoothFindFirstRadio(&m_bt_find_radio, &m_radio);
				m_search_params.hRadio = m_radio;
				ZeroMemory(&m_device_info, sizeof(BLUETOOTH_DEVICE_INFO));
				m_device_info.dwSize = sizeof(BLUETOOTH_DEVICE_INFO);
				m_bt_dev = BluetoothFindFirstDevice(&m_search_params, &m_device_info);
				if (m_bt_dev == NULL)
					exit(1);
				do{
					sprintf(buffer, "%02X%02X%02X%02X%02X%02X", m_device_info.Address.rgBytes[5],
						m_device_info.Address.rgBytes[4], m_device_info.Address.rgBytes[3], m_device_info.Address.rgBytes[2],
						m_device_info.Address.rgBytes[1], m_device_info.Address.rgBytes[0]);

					if (deviceAddress[0] == buffer[0] && deviceAddress[1] == buffer[1] && deviceAddress[2] == buffer[2]){
					deviceName = gcnew System::String(m_device_info.szName);
					break;
					}
				} while (BluetoothFindNextDevice(m_bt_dev, &m_device_info));*/
			}
	};
}
