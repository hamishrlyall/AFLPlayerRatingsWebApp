import React from "react";
import { Modal } from "react-bootstrap";
import EditPlayerModel from './edit-player'

const CreatePlayerModel = (props) => {


    return (
        <>
            <Modal show={props.show} onHide={props.handleClose} backdrop="static" keyboard={false} centered>
                <Modal.Header closeButton>
                    <Modal.Title>Add New Player</Modal.Title>
                </Modal.Header>

                <Modal.Body>
                    <EditPlayerModel/>
                </Modal.Body>
            </Modal>
        </>
    )
}

export default CreatePlayerModel;