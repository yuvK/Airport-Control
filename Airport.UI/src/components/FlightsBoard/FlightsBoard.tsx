import { useContext } from "react";
import './FlightsBoard.css';
import { airportContext } from "../../App";
import { Table } from "../table/Table";

export function FlightsBoard() {
    const { flightsList } = useContext(airportContext);

    return <>
        <div className="FlightsBoard">
            <h2>Flights Board:</h2>
            <Table {...{ flightsList }} />

        </div>
    </>
}