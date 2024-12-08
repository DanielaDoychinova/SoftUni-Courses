import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '30s', target: 10},
        { duration: '1m', target: 10},
        { duration: '10s', target: 0},
    ]
};

export default function (){
    const res = http.get('http://localhost:5000/api/enquiry/674f4576c96d1c8f65dc5017');
    check(res, {
        'status code is 200': (r) => r.status === 200,
        'contains expected fields': (r) => {
            const body = JSON.parse(r.body);
            return body.hasOwnProperty('_id') && body.hasOwnProperty('status')
        }
    });
    sleep(1)
}