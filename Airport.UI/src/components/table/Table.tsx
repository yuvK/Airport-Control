import './Table.css'
import { IAirplaneDTO } from "../../Interfaces/IAirplaneDTO";


export const Table = ({flightsList}: { flightsList :IAirplaneDTO[]}) => {



    return <>
        <div className='Table'>
            <table>
                <thead>
                    <tr>
                        <th id="time">ID</th>
                        <th id="destination">Flight</th>
                        <th id="flight">Depart/Arrival</th>
                    </tr>
                </thead>
                <tbody>
                    {flightsList.map((x) => (
                        <tr key={x.airplaneCode} >
                            <td>{x.airplaneId}</td>
                            <td>{x.airplaneCode}</td>
                            <td>{x.airplaneIsDeparture ? "Departure" : "Arrival"}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    </>
}