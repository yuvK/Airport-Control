

import { IAirplaneDTO } from '../../Interfaces/IAirplaneDTO';
import './Airplane.css'

export const Airplane = ({ /*airplaneId,*/ airplaneCode, airplaneIsDeparture }: IAirplaneDTO) => {
    //console.log(airplaneId)

    let isDeparted = airplaneIsDeparture;


    return <>
        {isDeparted
            ?
            <span className="material-symbols-outlined airplaneDepart">
                flight_takeoff
            </span>
            : <span className="material-symbols-outlined airplaneArrive">
                flight_land
            </span>
        }
        <b>{airplaneCode}</b>
    </>
}