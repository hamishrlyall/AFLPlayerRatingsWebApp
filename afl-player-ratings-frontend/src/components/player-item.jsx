import React from "react";
import { Col, Row } from "react-bootstrap";
import Headshot from "../headshot.png"

const PlayerItem = (props) => {

    return (
        <>
            <Row>
                <Col item xs={12} md={2}>
                    <img src={props.data.headshot || Headshot} style={{width: 150, height: 150}}></img>
                </Col>
                <Col item xs={12} md={10}>
                    <div><b>{props.data.name}</b></div>
                    <div>DOB: {props.data.formattedBirthdate}</div>
                    <div>Team: {props.data.team.name}</div>
                    <div>Rating: {props.data.rating}</div>
                </Col>
                <Col>
                    <hr/>
                </Col>
            </Row>
        </>
    )
}

export default PlayerItem;