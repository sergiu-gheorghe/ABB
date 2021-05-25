import {Table} from 'react-bootstrap'
import React, {useState, useEffect} from 'react'

export default function Users() {
    const[users, setData] = useState([]);
    const[name, setName] = useState('');
    const[userId, setUserId] = useState(null);

    const handleNameChange = e => setName(e.target.value);
    const handleUserIdChange = e => setUserId(e.target.value);

    useEffect(() => {
        let url = 'https://localhost:55002/api/v1/users?';
        if(name){
            url +=`name=${name}`;
        } 

        if(userId){
            url +=`&userId=${userId}`;
        }

        url.replace('?&', '?');

        fetch(url)
        .then((response) => response.json())
        .then((json) => setData(json))
        .catch(err => console.log(err.message));
    }, [name, userId]);

    return (
        <React.Fragment>
            <div className="container">
                <br/>
                <span>User Id:</span><input type="text" value={userId} onChange={handleUserIdChange}/>
                <span>Name:</span><input type="text" value={name} onChange={handleNameChange}/>
            </div>
            <Table className="mt-4" striped bordered hover size="sm">
                <thead>
                    <th>Id</th>
                    <th>Name</th>
                    <th>Username</th>
                    <th>Email</th>
                    <th>Street</th>
                    <th>Suite</th>
                    <th>City</th>
                    <th>Phone</th>
                    <th>Website</th>
                </thead>
                <tbody>
                    {
                        users.map(u => 
                            <tr key={u.id}>
                                <td>{u.id}</td>
                                <td>{u.name}</td>
                                <td>{u.username}</td>
                                <td>{u.email}</td>
                                <td>{u.address.street}</td>
                                <td>{u.address.suite}</td>
                                <td>{u.address.city}</td>
                                <td>{u.phone}</td>
                                <td>{u.website}</td>
                            </tr>
                            )
                    }
                </tbody>
            </Table>
        </React.Fragment>
    )
}