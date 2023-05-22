import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { useEffect, useRef, useState } from "react";
import { IStationDTO } from "../Interfaces/IStationDTO";
import { IAirplaneDTO } from "../Interfaces/IAirplaneDTO";

export function useSignalR() {
    const connection = useRef<HubConnection>(0 as any);

    const [stationDataQueue, setStationDataQueue] = useState<IStationDTO[]>([]);
    const [flightsList, setFlightsList] = useState<IAirplaneDTO[]>([]);
    try {
        connection.current = new HubConnectionBuilder()
            .withUrl("http://localhost:5143/AirportHub").configureLogging(LogLevel.Information).build();

        connection.current.on("StationsUpdate", (text: string) => {
            const data: IStationDTO = JSON.parse(text);
            //console.log(data);
            setStationDataQueue(x => ([...x, data]));
        });
        connection.current.on("AirplainsUpdate", (text: string) => {
            const airplanes:IAirplaneDTO[] = JSON.parse(text);
            //console.log(airplanes);
            setFlightsList(airplanes);
        });

        useEffect(() => {
            connection.current.stop().then(() => {
                connection.current.start().then(() => {
                    console.log("Connection started")
                });
            })

            return () => {
                connection.current.stop().then(() => { console.log("Connection Stoped") });
            };
        }, []);
    }
    catch (e) {
        console.log(e);
    }
    return { stationDataQueue, flightsList };
}