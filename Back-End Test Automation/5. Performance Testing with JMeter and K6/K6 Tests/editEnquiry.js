import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
    stages: [
        { duration: '30s', target: 10 },
        { duration: '1m', target: 10 },
        { duration: '10s', target: 0 },
    ]
};

export default function () {

    const enquiryId = '674f4576c96d1c8f65dc5004';

    const payload = {
        name: 'Some Name Updated',
        email: 'some@email.updated',
        mobile: '+987654321',
        comment: 'Some updated comment here.',
        status: 'Submitted'
    };

    const adminToken = 'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjY3NGY0NTc2Yzk2ZDFjOGY2NWRjNGY0OCIsImlhdCI6MTczMzI1MDc3MCwiZXhwIjoxNzMzMzM3MTcwfQ.HJaBJWw2Ng_PCblSKD1A8SBLV240vvm11baDzY71dYQ';


    const params = {
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${adminToken}`,
        },
    };

    const res = http.put(`http://localhost:5000/api/enquiry/${enquiryId}`, JSON.stringify(payload), params);
    check(res, {
        'status code is 200': (r) => r.status === 200,
        'contains expected fields': (r) => {
            const body = JSON.parse(r.body);
            return pa.name === payload.name && body.status === payload.status
        }
    });
    sleep(1)
}