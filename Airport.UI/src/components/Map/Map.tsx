import { useContext, useEffect, useState } from 'react';
import './Map.css';
import { Station } from '../Station/Station';
import { airportContext } from '../../App';
import { IStation } from '../../Interfaces/IStation';
import { IStationDTO } from '../../Interfaces/IStationDTO';

export function Map() {
    const { stationDataQueue } = useContext(airportContext);

    const [stations, setStations] = useState<{ map: IStation, data: IStationDTO }[]>([
        { map: { id: 1, name: 'Station 1', x: 5, y: 1, width: 1, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 2, name: 'Station 2', x: 4, y: 1, width: 1, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 3, name: 'Station 3', x: 3, y: 1, width: 1, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 4, name: 'Station 4', x: 2, y: 2, width: 1, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 5, name: 'Station 5', x: 1, y: 3, width: 2, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 6, name: 'Station 6', x: 1, y: 4, width: 2, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 7, name: 'Station 7', x: 3, y: 4, width: 1, height: 3 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 8, name: 'Station 8', x: 3, y: 3, width: 3, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
        { map: { id: 9, name: 'Station 9', x: 1, y: 1, width: 3, height: 1 }, data: { stationId: 1, airplaneId: 0, airplaneCode: "", airplaneIsDeparture: false } },
    ]);

    const stationsUIUpdate = (stationId: number, airplaneCode: string, airplaneIsDeparture: boolean) => {
        stations[stationId - 1].data.airplaneCode = airplaneCode;
        stations[stationId - 1].data.airplaneIsDeparture = airplaneIsDeparture;

        setStations(p => ([...p]))

    }

    const onChange = () => {
        setTimeout(() => {
            const data = stationDataQueue.shift();
            if (data !== undefined) {
                console.log(data);
                stationsUIUpdate(data.stationId, data.airplaneCode, data.airplaneIsDeparture)
                if (stationDataQueue.length > 0) {
                    onChange();
                }
            }
        }, 50)
    }

    useEffect(() => {
        onChange()
    }, [stationDataQueue])

    return (
        <div className="map">
            {stations.map(station => (
                <Station
                    key={station.map.id}
                    station={station.map}
                    airplane={{
                        airplaneId: station.data.airplaneId,
                        airplaneCode: station.data.airplaneCode,
                        airplaneIsDeparture: station.data.airplaneIsDeparture
                    }}
                />
            ))}
        </div>
    );
};