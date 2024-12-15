import React, { useState } from "react";
import { Button, Col, Row } from "react-bootstrap";
import PlayerList from "../components/player-list";
import CreatePlayerModel from "../components/create-player-model";

const Landing = () => {
    const[show, setShow] = useState(false);

    return (
        <>
            <Row>
                <Col xs={12} md={10}>
                    <h2>Players</h2>
                </Col>
                <Col xs={12} md={2} classname="align-self-center">
                    <Button classname="float-right" onClick={() => setShow(true)}>Add New Player</Button>
                </Col>
            </Row>

            <PlayerList>
                
            </PlayerList>
            <CreatePlayerModel show={show} handleClose={() => setShow(false)}/>
        </>
    )
}

export default Landing;