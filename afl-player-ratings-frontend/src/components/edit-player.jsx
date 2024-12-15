import React, { useState } from "react";
import { Button, Form, Image } from "react-bootstrap";
import Headshot from "../headshot.png"
import AsyncSelect from "react-select/async";

const EditPlayerModel = (props) => {
    const[player, setPlayer] = useState({});
    const[team, setTeam] = useState({
        id: -1,
        name: ""
    });
    const[validated, setValidated] = useState(false);

    const handleFileUpload = (event) => {
        event.preventDefault();
        var file = event.target.files[0];
        const form = new FormData();
        form.append("_ImageFile", file);

        fetch( process.env.REACT_APP_API_URL + "/Player/upload-player-headshot", {
            method: "POST",
            body: form
        })
        .then( res =>  {
            console.log('res', res)
            return res.json()
        })
        .then(res => {
            console.log('res', res)
            var newData = player;
            newData.headshot = res.profileImage;

            setPlayer(oldData => {return{...oldData, ...newData}; });
        })
        .catch( err => alert( "Error in file upload" ));
    }

    const handleSave = (event) => {
        event.preventDefault();
        const form = event.currentTarget;
        if(form.checkValidity() === false){
            event.stopPropagation();
            setValidated(true);
            return;
        }

        let playerToPost = player;

        if( player && player.id > 0 ){
            // update
            fetch(process.env.REACT.APP.API.URL + "/Player", {
                method: "PUT",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(playerToPost)
            })
            .then( res => res.json())
            .then(res => {
                if( res.status === true && res.data ){
                    setPlayer(res.data);
                    alert('updated successfully.');
                }
            } )
            .catch( err => alert( "Error in file upload" ));
        }
        else{
            let teamid = team.id;
            console.log("TeamId = " + teamid);
            console.log( "Create" );
            // create
            fetch(process.env.REACT_APP_API_URL + "/Player?_TeamId=" + team.id, {
                method: "POST",
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(playerToPost)
            })
            .then( res => res.json())
            .then(res => {
                if( res.status === true && res.data ){
                    let playerData = res.data;
                    if( 
                        playerData.birthDate !== null &&
                        playerData.birthDate !== undefined
                    ) {
                        playerData.birthDate = playerData.birthDate.split("T")[0];
                    }
                    setPlayer(res.data);
                    alert('created successfully.');
                }
            } )
            .catch( err => alert( "Error getting data" ));
        }
    }

    const handleFieldChange = (event) => {
        var newData = player;
        newData[event.target.name] = event.target.value;

        setPlayer( oldData => {return{...oldData, ...newData}; });
    }

    const teamPromiseOptions = (inputValue) => {
        return fetch(process.env.REACT_APP_API_URL + "/Team/GetTeamsBySearchValue/" + inputValue )
            .then((res) => res.json())
            .then((res) => {
                if( res.status === true && res.data.length  > 0 ){
                    return res.data.map((x) => {
                        return { id: x.id, name: x.name };
                    });
                }

                if( res.data.count === 0){
                    alert( "there is no team matching this name.");
                }
            })
            .catch( (err) => alert("Error getting data"));
    };

    return (
        <>
            <Form noValidate validated={validated} onSubmit={handleSave}>
                <Form.Group className="d-flex justify-content-center">
                    <Image width="200" height="200" src={player && player.headshot || Headshot}/>
                </Form.Group>
                <Form.Group className="d-flex justify-content-center">
                    <div><input type="file" onChange={handleFileUpload}/></div>
                </Form.Group>
                <Form.Group controlId="formplayerName">
                    <Form.Label> Player Name</Form.Label>
                    <Form.Control name="name" value={player && player.name || ''} required type="text" autoComplete="" placeholder="Enter Player Name" onChange={handleFieldChange}/>
                    <Form.Control.Feedback typeof="invalid">
                        Please enter player name.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="formplayerBirthDate">
                    <Form.Label> Date of Birth</Form.Label>
                    <Form.Control name="birthDate" value={player && player.birthDate || ''} required type="date" autoComplete="" onChange={handleFieldChange}/>
                    <Form.Control.Feedback typeof="invalid">
                        Please enter player birth date.
                    </Form.Control.Feedback>
                </Form.Group>
                <Form.Group controlId="formplayerTeam">
                    <Form.Label>Team</Form.Label>
                    <AsyncSelect 
                    cacheOptions 
                    value={team} 
                    loadOptions={teamPromiseOptions} 
                    required 
                    onChange={(selectedOption) => {
                        setTeam({
                            id: selectedOption?.id || -1,
                            name: selectedOption?.name || ""
                        });
                    }}
                    />
                    <Form.Control.Feedback typeof="invalid">
                        Please enter players team.
                    </Form.Control.Feedback>
                </Form.Group>
                <Button type="submit"> {player && player.id > 0 ? "Update" : "Create"}</Button>
            </Form>
        </>
    )
}

export default EditPlayerModel;