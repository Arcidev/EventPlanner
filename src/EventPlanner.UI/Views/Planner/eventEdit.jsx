import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";
import Pusher from "pusher-js";
import GoogleMap from 'google-map-react';

import 'bootstrap/dist/css/bootstrap.css';
import '../../Styles/site.css';
import {getBaseUrl} from './commonScript.jsx';

var eventData = {
    places: [{ lat: 59.938043, lng: 30.337157 }, { lat: 59.938, lng: 30.33 }]
}

class BasicInfoBlock extends React.Component {

    constructor(props) {
        super(props);

        this.state = { 
            name : "Event Name",
            desc : "Event Description"
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                name: response.data.name,
                desc: response.data.desc
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    render(){
        return(
                <form className="form-horizontal">
                    <div className="form-group">
                        <label htmlFor="eventName" className="col-sm-2 control-label">Name</label>
                        <div className="col-sm-10">
                        <input type="text" id="eventName" key={this.state.name} className="form-control" placeholder="Event name" defaultValue={this.state.name}/>
                        </div>
                    </div>
                    <div className="form-group">
                        <label htmlFor="eventDesc" className="col-sm-2 control-label">Description</label>
                        <div className="col-sm-10">
                        <textarea id="eventDesc" key={this.state.desc} className="form-control" rows="3" defaultValue={this.state.desc}></textarea>
                        </div>
                    </div>
                    <div className="form-group">
                        <div className="col-sm-offset-2 col-sm-10">
                        <button type="submit" className="btn btn-default">Save event</button>
                        </div>
                    </div>
                </form>
            );
    }
}


class PeopleRows extends React.Component {
    constructor(props) {
        super(props);

        this.state = { 
            people : ["john.smith@myexample.com"],
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                people: response.data.people
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }


    render() {
        var rows = [];
        var count = 1;
        this.state.people.forEach(function (row)
        {
            var rowId = "personEmail" + count;
            rows.push
            (
                    <div className="form-group">
                        <label htmlFor={rowId} className="col-sm-2 control-label">Person's email</label>
                        <div className="col-sm-10">
                            <input type="email" id={rowId} className="form-control" value={row} />
                        </div>
                    </div>
            );
            count++;
        });

     
        return (
            
        <div>/*
            <div className="form-group">
                <label htmlFor="personEmail0" className="col-sm-2 control-label">Person's email</label>
                <div className="col-sm-10">
                <input type="email" id="personEmail0" className="form-control" placeholder="john.smith@example.com" />
                </div>
            </div>*/
            {rows}
        </div>
        );
    }
}

class PeopleBlock extends React.Component {
    render(){
        return(
            <form className="form-horizontal">
                <button type="button" className="btn btn-default">Add people</button>
                <PeopleRows/>
            </form>
        );
    }
}

class DateTimeBlock extends React.Component{
    constructor(props) {
        super(props);

        this.state = { 
            dates : ["2016-01-01T10:10:00"],
        };
    }

    componentDidMount() {
        axios
        .get(getBaseUrl()+`get`)
        .then((response) => {
            this.setState({
                dates: response.data.dates
            });
        })
        .catch((e) => 
        {
            console.error(e);
        });
    }

    render(){
        var rows = [];
        var count = 1;
        this.state.dates.forEach(function(date){
            var rowId = "eventDate" + count;
            rows.push
            (
                <div className="form-group">
                    <label htmlFor={rowId} className="col-sm-2 control-label">Datetime</label>
                    <div className="col-sm-10">
                    <input type="datetime-local" id={rowId} className="form-control" defaultValue={date} value={date}/>
                    </div>
                </div>
            );
            count++;
        })

        return(
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add date</button>
                    /*
                    <div className="form-group">
                        <label htmlFor="eventDate0" className="col-sm-2 control-label">Datetime</label>
                        <div className="col-sm-10">
                        <input type="datetime-local" id="eventDate0" className="form-control"/>
                        </div>
                    </div>*/
                    {rows}
                </form>
        );
    }
}

class PlaceBlock extends React.Component{
    render(){

        var center = { lat: 59.938043, lng: 30.337157 };
        var zoom = 9;

        return(
            <div>
                <div className="thumbnail ep-map">
                    <GoogleMap defaultCenter={center}
                            defaultZoom={zoom}>
                        <div className="ep-marker">place A</div>
                    </GoogleMap>
                </div>
                <form className="form-horizontal">
                    <button type="button" className="btn btn-default">Add place</button>
                    <div className="form-group">
                        <label htmlFor="eventPlace0" className="col-sm-2 control-label">Place</label>
                        <div className="col-sm-10">
                        <input type="text" id="eventPlace0" className="form-control" />
                        </div>
                    </div>
                </form>
            </div>
            );
    }
}

class EventEditLayout extends React.Component {
    render() {

        let styles = {
                panel: {
                border: "solid #f1f1f1",
                borderWidth: "1 1px",
                borderRadius: "8px",

                padding: "15px"
                }
        }

        return (
            <div>

            <h2>Basic information</h2>
                <div style={styles.panel} >
                <BasicInfoBlock/>
                </div>
            <h2>People</h2>
                <div style={styles.panel} >
                <PeopleBlock/>
                </div>
            <h2>Date and Time</h2>
                <div style={styles.panel} >
                <DateTimeBlock/>
                </div>
            <h2>Place</h2>
                <div style={styles.panel} >
                <PlaceBlock/>
                </div>
            </div>
        );
    }
}

const eventEdit = document.getElementById('event-edit');
if (eventEdit) {
    ReactDOM.render(<EventEditLayout />, eventEdit);
}